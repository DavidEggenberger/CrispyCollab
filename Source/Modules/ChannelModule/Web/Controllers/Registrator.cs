using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelServerModule
{
    public static class Registrator
    {
        public static IServiceCollection RegisterChannelModuleServer(this IServiceCollection serviceCollection)
        {
            ////IMvcBuilder mvcBuilder = serviceCollection.BuildServiceProvider().GetRequiredService<IMvcBuilder>();
            ////mvcBuilder.AddApplicationPart(typeof(Registrator).Assembly);
            serviceCollection.AddControllers().AddApplicationPart(typeof(Registrator).Assembly);
            

            return serviceCollection;
        }
    }
}
