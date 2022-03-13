using Domain.SharedKernel;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Types;
using Infrastructure.Identity.Types.Enums;
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
        public Subscription Subscription { get; set; }
        public Guid CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
        public List<ApplicationUser> SelectedByUsers { get; set; }
        public List<AdminNotification> Notifications { get; set; }

        private List<ApplicationUserTeam> members = new List<ApplicationUserTeam>();
        public IReadOnlyCollection<ApplicationUserTeam> Members => members.AsReadOnly();
        private Team() { }
        public Team(ApplicationUser creator, Subscription subscription, string name)
        {
            Creator = creator;
            Name = name;
            members = new List<ApplicationUserTeam>
            {
                new ApplicationUserTeam
                {
                    Role = TeamRole.Admin,
                    User = creator,
                    Status = MembershipStatus.Joined
                }
            };
            Subscription = subscription;
        }
        public void InviteMember(ApplicationUser applicationUser, TeamRole teamRole)
        {
            if (members.Any(m => m.UserId == applicationUser.Id) is false)
            {
                members.Add(new ApplicationUserTeam
                {
                    Role = teamRole,
                    User = applicationUser,
                    Status = MembershipStatus.Invited
                });
            }
            else
            {
                throw new IdentityOperationException("The user is already a member");
            }
        }
        public void RevokeInvitation(ApplicationUser applicationUser)
        {
            ApplicationUserTeam applicationUserTeam;
            try
            {
                applicationUserTeam = members.Single(x => x.UserId == applicationUser.Id && x.Status == MembershipStatus.Invited);
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException("The user is already a member");
            }
            members.Remove(applicationUserTeam);
        }
        public void AddMember(ApplicationUser applicationUser, TeamRole teamRole)
        {
            ApplicationUserTeam applicationUserTeam = members.SingleOrDefault(x => x.UserId == applicationUser.Id);
            if(applicationUserTeam == null)
            {
                members.Add(new ApplicationUserTeam
                {
                    Role = teamRole,
                    User = applicationUser,
                    Status = MembershipStatus.Joined
                });
            }
            else
            {
                applicationUserTeam.Status = MembershipStatus.Joined;
            }
        }
        public void ChangeRoleOfMember(ApplicationUser applicationUser, TeamRole teamRole)
        {
            if(Creator.Id == applicationUser.Id && teamRole != TeamRole.Admin)
            {
                throw new IdentityOperationException("The Creator's role cant be changed from admin");
            }
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
            if(applicationUser.Id == Creator.Id)
            {
                throw new IdentityOperationException("The Creator of the Team cannot be removed");
            }
            members.Remove(members.Single(m => m.UserId == applicationUser.Id));
        }
    }
}
