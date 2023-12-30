using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.Features.Aggregates.UserAggregate;
using Shared.Features.CQRS.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.Features.Aggregates.UserAggregate.Application.Commands
{
    public class CreateUserCommand : ICommand
    {
        public ApplicationUser User { get; set; }
        public ExternalLoginInfo LoginInfo { get; set; }
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserManager userManager;
        private readonly ICommandDispatcher commandDispatcher;

        public CreateUserCommandHandler(SignInManager<ApplicationUser> signInManager, ApplicationUserManager userManager, ICommandDispatcher commandDispatcher, IStripeCustomerService stripeCustomerService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.commandDispatcher = commandDispatcher;
        }
        public async Task HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await userManager.CreateAsync(command.User);

            if (result.Succeeded)
            {
                await userManager.AddLoginAsync(command.User, command.LoginInfo);
                await signInManager.SignInAsync(command.User, isPersistent: false, command.LoginInfo.LoginProvider);

                //var stripeCustomer = await stripeCustomerService.CreateStripeCustomerAsync(command.User.UserName, command.User.Email);
                //command.User.StripeCustomerId = stripeCustomer.Id;

                //await userManager.UpdateAsync(command.User);
            }
        }
    }
}
