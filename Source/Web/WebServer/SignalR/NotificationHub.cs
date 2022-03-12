using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Infrastructure.Identity.Types.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private ApplicationUserManager applicationUserManager;
        private IdentificationDbContext identificationDbContext;
        public NotificationHub(ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext)
        {
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
        }
        public override async Task OnConnectedAsync()
        {
            ApplicationUser appUser = await applicationUserManager.FindByClaimsPrincipalAsync(Context.User);
            //await Groups.AddToGroupAsync(Context.ConnectionId, $"{applicationUserTeam.TeamId}{applicationUserTeam.Role}");
            if (appUser.IsOnline is false)
            {
                appUser.IsOnline = true;
                appUser.TabsOpen = 1;
                await applicationUserManager.UpdateAsync(appUser);
                await Clients.All.SendAsync("UpdateOnlineUsers");
                return;
            }
            if (appUser.IsOnline)
            {
                appUser.TabsOpen++;
                await applicationUserManager.UpdateAsync(appUser);
            }
        }
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            ApplicationUser appUser = await applicationUserManager.FindByClaimsPrincipalAsync(Context.User);
            if (appUser.TabsOpen > 0)
            {
                appUser.TabsOpen--;
                await applicationUserManager.UpdateAsync(appUser);
            }
            if (appUser.TabsOpen == 0)
            {
                appUser.IsOnline = false;
                await applicationUserManager.UpdateAsync(appUser);
                //ApplicationUserTeam applicationUserTeam = appUser.Memberships.Single(x => x.TeamId == appUser.SelectedTeam.Id);
                //await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{applicationUserTeam.TeamId}{applicationUserTeam.Role}");
                await Clients.AllExcept(appUser.Id.ToString()).SendAsync("UpdateOnlineUsers");
            }
        }
    }
}
