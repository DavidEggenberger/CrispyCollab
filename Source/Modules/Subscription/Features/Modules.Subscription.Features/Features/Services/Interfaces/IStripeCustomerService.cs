namespace Modules.Subscriptions.Features.Services.Interfaces
{
    public interface IStripeCustomerService
    {
        Task<Customer> CreateStripeCustomerAsync(string userName, string emailAddress);
        Task DeleteStripeCustomerAsync(string stripeCustomerId);
        Task<Customer> UpdateStripeCustomerAsync(string stripeCustomerId);
    }
}
