using WebShared.Identity.Team.AdminManagement;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWasmClient.Components.ManageTeam.Notifications
{
    public partial class AdminNotificationsComp
    {
        [Parameter] public List<AdminNotificationDTO> AdminNotifications { get; set; }
        private List<AdminNotificationDTO> sortedAdminNotifications;
        private Dictionary<string, Action> dateFilterings;
        private string typeSorter;
        public string TypeSorter
        {
            get => typeSorter;
            set 
            { 
                typeSorter = value;
                if(value == "All")
                {
                    sortedAdminNotifications = AdminNotifications.OrderByDescending(x => x.CreatedAt).ToList();
                }
                else
                {
                    sortedAdminNotifications = sortedAdminNotifications.Where(x => x.Type == (AdminNotificationTypeDTO)Enum.Parse(typeof(AdminNotificationTypeDTO), value)).ToList();
                }
                StateHasChanged();
            }
        }
        private string dateSorter;
        public string DateSorter
        {
            get => dateSorter;
            set 
            { 
                dateSorter = value;
                dateFilterings[value]();
                StateHasChanged();
            }
        }
        protected override async Task OnInitializedAsync()
        {
            sortedAdminNotifications = AdminNotifications.OrderByDescending(x => x.CreatedAt).ToList();
            dateFilterings = new Dictionary<string, Action>()
            {
                ["newest"] = () => { sortedAdminNotifications = sortedAdminNotifications.OrderByDescending(x => x.CreatedAt).ToList(); },
                ["oldest"] = () => { sortedAdminNotifications = sortedAdminNotifications.OrderBy(x => x.CreatedAt).ToList(); },
            };
        }
    }
}
