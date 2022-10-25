using Shared.Modules.Layers.Application.CQRS.Command;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Modules.Layers.Infrastructure.CQRS.Command;
using Modules.IdentityModule.Domain;
using Modules.TenantIdentityModule.Domain;

namespace Shared.Modules.Layers.Infrastructure.Identity.Commands
{
    public class CreateUserCommand : ICommand
    {
        public ApplicationUser User { get; set; }
        public ExternalLoginInfo LoginInfo { get; set; }
    }
}
