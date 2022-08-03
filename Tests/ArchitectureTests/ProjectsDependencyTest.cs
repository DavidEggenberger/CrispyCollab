using FluentAssertions;
using Xunit;

namespace ArchitectureTests
{
    public class ProjectsDependencyTest
    {
        [Fact]
        public void AssemblyReferencesMustBeCorrect()
        {
            var domainAssembly = typeof(Domain.IAssemblyMarker).Assembly;
            var infrastructureAssembly = typeof(Infrastructure.IAssemblyMarker).Assembly;
            var applicationAssembly = typeof(Application.IAssemblyMarker).Assembly;
            var webAPIAssembly = typeof(WebServer.IAssemblyMarker).Assembly;
            var webClientAssembly = typeof(WebWasmClient.Misc.IAssemblyMarker).Assembly;
            var webSharedAssembly = typeof(WebShared.IAssemblyMarker).Assembly;

            domainAssembly.Should().NotReference(infrastructureAssembly);
            domainAssembly.Should().NotReference(applicationAssembly);
            domainAssembly.Should().NotReference(webAPIAssembly);
            domainAssembly.Should().NotReference(webClientAssembly);
            domainAssembly.Should().NotReference(webSharedAssembly);

            infrastructureAssembly.Should().NotReference(applicationAssembly);
            infrastructureAssembly.Should().NotReference(webAPIAssembly);
            infrastructureAssembly.Should().NotReference(webClientAssembly);
            infrastructureAssembly.Should().NotReference(webSharedAssembly);

            applicationAssembly.Should().NotReference(webAPIAssembly);
            applicationAssembly.Should().NotReference(webClientAssembly);
            applicationAssembly.Should().NotReference(webSharedAssembly);

            webSharedAssembly.Should().NotReference(webAPIAssembly);
            webSharedAssembly.Should().NotReference(applicationAssembly);

            webClientAssembly.Should().NotReference(webAPIAssembly);
        }
    }
}
