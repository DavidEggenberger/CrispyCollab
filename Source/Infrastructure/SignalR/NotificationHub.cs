using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR
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
            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(Context.User);
            //ApplicationUserTeam applicationUserTeam = await applicationUserManager.GetCurrentTeamMembership(applicationUser);
            //await Groups.AddToGroupAsync(Context.ConnectionId, $"{applicationUserTeam.TeamId}{applicationUserTeam.Role}");
            //await Groups.AddToGroupAsync(Context.ConnectionId, $"{applicationUserTeam.TeamId}");
            if (applicationUser.IsOnline is false)
            {
                applicationUser.IsOnline = true;
                applicationUser.TabsOpen = 1;
                await applicationUserManager.UpdateAsync(applicationUser);
                await Clients.All.SendAsync("UpdateOnlineUsers");
                return;
            }
            if (applicationUser.IsOnline)
            {
                applicationUser.TabsOpen++;
                await applicationUserManager.UpdateAsync(applicationUser);
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
                ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(Context.User);
                //ApplicationUserTeam applicationUserTeam = await applicationUserManager.GetCurrentTeamMembership(applicationUser);
                //await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{applicationUserTeam.TeamId}{applicationUserTeam.Role}");
                //await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{applicationUserTeam.TeamId}");
                await Clients.AllExcept(appUser.Id.ToString()).SendAsync("UpdateOnlineUsers");
            }
        }
    }
}
