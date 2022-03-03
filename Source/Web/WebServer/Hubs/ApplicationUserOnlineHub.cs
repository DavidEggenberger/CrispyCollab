using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebServer.Hubs
{
    public class ApplicationUserOnlineHub : Hub
    {
        private ApplicationUserManager applicationUserManager;
        private IdentificationDbContext identificationDbContext;
        public ApplicationUserOnlineHub(ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext)
        {
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
        }
        public override async Task OnConnectedAsync()
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                return;
            }
            ApplicationUser appUser = await applicationUserManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
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
            if (!Context.User.Identity.IsAuthenticated)
            {
                return;
            }
            ApplicationUser appUser = await applicationUserManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (appUser.TabsOpen > 0)
            {
                appUser.TabsOpen--;
                await applicationUserManager.UpdateAsync(appUser);
            }
            if (appUser.TabsOpen == 0)
            {
                appUser.IsOnline = false;
                await applicationUserManager.UpdateAsync(appUser);
                await Clients.AllExcept(appUser.Id.ToString()).SendAsync("UpdateOnlineUsers");
            }
        }
    }
}
