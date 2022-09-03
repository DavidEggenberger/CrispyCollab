using Infrastructure.Identity;
using Infrastructure.StripePayments.Models;
using Infrastructure.StripePayments.Services.Interfaces;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text.Json;

namespace Infrastructure.StripePayments.Services
{
    public class StripeSubscriptionService : IStripeSubscriptionService
    {
        public StripeSubscription GetSubscriptionFromPlanType(SubscriptionPlanType subscriptionPlanType)
        {
            var manifestEmbeddedProvider = new ManifestEmbeddedFileProvider(typeof(IAssemblyMarker).Assembly);
            var fileInfo = manifestEmbeddedProvider.GetFileInfo("StripePayments/configuration/stripeconfiguration.json");
            using var reader = new StreamReader(fileInfo.CreateReadStream());

            var fileContent = reader.ReadToEnd();

            var subscriptions = JsonSerializer.Deserialize<List<StripeSubscription>>(fileContent,
                   new JsonSerializerOptions
                   {
                       PropertyNameCaseInsensitive = true
                   });

            return subscriptions.Single(s => s.Type == subscriptionPlanType);
        }
    }
}
