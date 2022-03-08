using Domain.SharedKernel;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Types;
using Infrastructure.Identity.Types.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public Guid? CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
        public List<ApplicationUser> SelectedByUsers { get; set; }

        private List<ApplicationUserTeam> members = new List<ApplicationUserTeam>();
        public IReadOnlyCollection<ApplicationUserTeam> Members => members.AsReadOnly();
        public void AddMember(ApplicationUser applicationUser, TeamRole teamRole)
        {
            if(members.Any(m => m.UserId == applicationUser.Id) is false)
            {
                members.Add(new ApplicationUserTeam
                {
                    Role = teamRole,
                    User = applicationUser
                });
            }
            else
            {
                throw new IdentityOperationException("");
            }
        }
        public void ChangeRoleOfMember(ApplicationUser applicationUser, TeamRole teamRole)
        {
            ApplicationUserTeam applicationUserTeam;
            try
            {
                applicationUserTeam = members.Single(m => m.UserId == applicationUser.Id);
                applicationUserTeam.Role = teamRole;
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException("");
            }
        }
        public void RemoveMember(ApplicationUser applicationUser)
        {
            try
            {
                members.Remove(members.Single(m => m.UserId == applicationUser.Id));
            }
            catch(Exception ex)
            {
                throw new IdentityOperationException("");
            }
        }
    }
}
