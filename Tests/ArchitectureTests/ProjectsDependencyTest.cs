using FluentAssertions;
using Xunit;

namespace ArchitectureTests
{
    public class ProjectsDependencyTest
    {
        [Fact]
        public void AssemblyReferencesMustBeCorrect()
        {
            

            //var Shared.Modules.Layers.InfrastructureAssembly = typeof(Shared.Modules.Layers.Infrastructure.IAssemblyMarker).Assembly;
            //var modulesAssembly = typeof(IAssemblyMarker).Assembly;
            //var webAPIAssembly = typeof(WebServer.IAssemblyMarker).Assembly;
            //var webClientAssembly = typeof(WebWasmClient.Misc.IAssemblyMarker).Assembly;
            //var webSharedAssembly = typeof(IAssemblyMarker).Assembly;

            //domainAssembly.Should().NotReference(Shared.Modules.Layers.InfrastructureAssembly);
            //domainAssembly.Should().NotReference(modulesAssembly);
            //domainAssembly.Should().NotReference(webAPIAssembly);
            //domainAssembly.Should().NotReference(webClientAssembly);
            //domainAssembly.Should().NotReference(webSharedAssembly);

            //Shared.Modules.Layers.InfrastructureAssembly.Should().NotReference(modulesAssembly);
            //Shared.Modules.Layers.InfrastructureAssembly.Should().NotReference(webAPIAssembly);
            //Shared.Modules.Layers.InfrastructureAssembly.Should().NotReference(webClientAssembly);
            //Shared.Modules.Layers.InfrastructureAssembly.Should().NotReference(webSharedAssembly);

            //modulesAssembly.Should().NotReference(webAPIAssembly);
            //modulesAssembly.Should().NotReference(webClientAssembly);
            //modulesAssembly.Should().NotReference(webSharedAssembly);

            //webSharedAssembly.Should().NotReference(webAPIAssembly);
            //webSharedAssembly.Should().NotReference(modulesAssembly);

            //webClientAssembly.Should().NotReference(webAPIAssembly);
        }
    }
}
