using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants
{
    public class Tenant : Entity
    {
        private Tenant() { }
        public Tenant(string name)
        {
            Name = name;
        }
        public Tenant(IServiceProvider serviceProvider)
        {

        }

        public string Name { get; set; }
        public TenantStyling Styling { get; set; }
        public TenantSettings Settings { get; set; }
        public SubscriptionPlanType CurrentSubscriptionPlanType { get; set; }
        public IReadOnlyCollection<TenantMembership> Memberships => memberships.AsReadOnly();
        private List<TenantMembership> memberships = new List<TenantMembership>();
        public IReadOnlyCollection<TenantInvitation> Invitations => invitations.AsReadOnly();
        private List<TenantInvitation> invitations = new List<TenantInvitation>();
        public IReadOnlyCollection<TenantSubscription> TenantSubscriptions => tenantSubscriptions.AsReadOnly();
        private List<TenantSubscription> tenantSubscriptions = new List<TenantSubscription>();

        public void AddUser(Guid userId, TenantRole role)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            TenantMembership tenantMembership;
            if ((tenantMembership = memberships.SingleOrDefault(m => m.UserId == userId)) is not null)
            {
                tenantMembership.Role = role;
            }
            else
            {
                memberships.Add(new TenantMembership(userId, role));
            }
        }
        public void ChangeRoleOfMember(Guid userId, TenantRole newRole)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId) is false)
            {
                //throw new MemberNotFoundException();
            }

        }
        public void RemoveUser(Guid userId)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId) is false)
            {
                //throw new MemberNotFoundException();
            }

            memberships.Remove(memberships.Single(m => m.UserId == userId));
        }
        public void InviteUserToRole(Guid userId, TenantRole role)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId))
            {
                //throw new UserIsAlreadyMemberException();
            }

            invitations.Add(new TenantInvitation { UserId = userId, Role = role });
        }
        public void AddSubscription(string stripeSubscriptionId, SubscriptionPlanType type, DateTime startDate, DateTime endDate, bool isTrial)
        {
            
        }
        public bool CheckIfMember(Guid userId)
        {
            return memberships.Any(membership => membership.UserId == userId);
        }
    }

    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.Navigation(b => b.Memberships)
                .HasField("memberships")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
