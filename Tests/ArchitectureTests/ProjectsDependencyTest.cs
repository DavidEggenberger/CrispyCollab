using Domain;
using FluentAssertions;
using Xunit;

namespace ArchitectureTests
{
    public class ProjectsDependencyTest
    {
        [Fact]
        public void AssemblyReferencesMustBeCorrect()
        {
            var sharedKernelAssembly = typeof(SharedKernel.IAssemblyMarker).Assembly;
            sharedKernelAssembly.Should().Reddddads

            //var infrastructureAssembly = typeof(Infrastructure.IAssemblyMarker).Assembly;
            //var modulesAssembly = typeof(IAssemblyMarker).Assembly;
            //var webAPIAssembly = typeof(WebServer.IAssemblyMarker).Assembly;
            //var webClientAssembly = typeof(WebWasmClient.Misc.IAssemblyMarker).Assembly;
            //var webSharedAssembly = typeof(IAssemblyMarker).Assembly;

            //domainAssembly.Should().NotReference(infrastructureAssembly);
            //domainAssembly.Should().NotReference(modulesAssembly);
            //domainAssembly.Should().NotReference(webAPIAssembly);
            //domainAssembly.Should().NotReference(webClientAssembly);
            //domainAssembly.Should().NotReference(webSharedAssembly);

            //infrastructureAssembly.Should().NotReference(modulesAssembly);
            //infrastructureAssembly.Should().NotReference(webAPIAssembly);
            //infrastructureAssembly.Should().NotReference(webClientAssembly);
            //infrastructureAssembly.Should().NotReference(webSharedAssembly);

            //modulesAssembly.Should().NotReference(webAPIAssembly);
            //modulesAssembly.Should().NotReference(webClientAssembly);
            //modulesAssembly.Should().NotReference(webSharedAssembly);

            //webSharedAssembly.Should().NotReference(webAPIAssembly);
            //webSharedAssembly.Should().NotReference(modulesAssembly);

            //webClientAssembly.Should().NotReference(webAPIAssembly);
        }
    }
}
