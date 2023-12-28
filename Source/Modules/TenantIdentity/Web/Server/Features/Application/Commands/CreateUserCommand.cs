using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.Domain;
using Shared.Features.CQRS.Command;

namespace Shared.Modules.Layers.Features.Identity.Commands
{
    public class CreateUserCommand : ICommand
    {
        public ApplicationUser User { get; set; }
        public ExternalLoginInfo LoginInfo { get; set; }
    }
}
