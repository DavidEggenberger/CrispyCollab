using Domain.SharedKernel;
using Infrastructure.Identity.Types.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string IconUri { get; set; }
        public string Name { get; set; }
        public TenantPlan Plan { get; set; }
        public List<TenantApplicationUser> Members { get; set; }
        public Tenant(string name, ApplicationUser creator)
        {

        }
        private Tenant()
        {

        }
        /// <summary>
        /// Change the role of the Member to the specified one
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <param name="tenantRoleType"></param>
        /// <returns></returns>
        public IdentityOperationResult ChangeRoleOfMember(ApplicationUser applicationUser, TenantRoleType tenantRoleType)
        {
            if (!Members.Any(tenantApplicationUser => tenantApplicationUser.ApplicationUser.Id == applicationUser.Id && tenantApplicationUser.Role == tenantRoleType))
            {
                if(Members.Where(x => x.Role == TenantRoleType.Admin).All(x => x.ApplicationUserId == applicationUser.Id) && tenantRoleType != TenantRoleType.Admin)
                {
                    return IdentityOperationResult.Fail("You cant delete the only admin");
                }
                Members.RemoveAll(tenantApplicationUser => tenantApplicationUser.ApplicationUserId == applicationUser.Id);
                Members.Add(new TenantApplicationUser
                {
                    ApplicationUser = applicationUser,
                    Role = tenantRoleType,
                    Tenant = this
                });
                return IdentityOperationResult.Success();
            }
            else
            {
                return IdentityOperationResult.Fail("The user has already the specified role");
            }
        }
        /// <summary>
        /// Adds a new Member with the Role User
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public IdentityOperationResult AddMember(ApplicationUser applicationUser)
        {
            if (!Members.Any(member => member.ApplicationUserId == applicationUser.Id))
            {
                Members.Add(new TenantApplicationUser
                {
                    ApplicationUser = applicationUser,
                    Role = TenantRoleType.User
                });
                return IdentityOperationResult.Success();
            }
            else
            {
                return IdentityOperationResult.Fail("The user is already a member");
            }
        }
        /// <summary>
        /// Adds a new Member with the specified Role
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <param name="tenantRoleType"></param>
        /// <returns></returns>
        public IdentityOperationResult AddMember(ApplicationUser applicationUser, TenantRoleType tenantRoleType)
        {
            if (!Members.Any(member => member.ApplicationUserId == applicationUser.Id))
            {
                Members.Add(new TenantApplicationUser
                {
                    ApplicationUser = applicationUser,
                    Role = tenantRoleType
                });
                return IdentityOperationResult.Success();
            }
            else
            {
                return IdentityOperationResult.Fail("The user is already a member");
            }
        }
    }
}
