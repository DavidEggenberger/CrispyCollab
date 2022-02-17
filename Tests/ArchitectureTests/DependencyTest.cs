using FluentAssertions;
using System;
using Xunit;

namespace ArchitectureTests
{
    public class DependencyTest
    {
        [Fact]
        public void AssemblyReferencesMustBeCorrect()
        {
            var domainAssembly = typeof(Domain.Misc.IAssemblyMarker).Assembly;
            var infrastructureAssembly = typeof(Infrastructure.Misc.IAssemblyMarker).Assembly;
            var applicationAssembly = typeof(Application.Misc.IAssemblyMarker).Assembly;
            var webAPIAssembly = typeof(WebAPI.Misc.IAssemblyMarker).Assembly;
            var webClientAssembly = typeof(WebClient.Misc.IAssemblyMarker).Assembly;
            var commonAssembly = typeof(Common.Misc.IAssemblyMarker).Assembly;

            domainAssembly.Should().NotReference(infrastructureAssembly);
            domainAssembly.Should().NotReference(applicationAssembly);
            domainAssembly.Should().NotReference(webAPIAssembly);
            domainAssembly.Should().NotReference(webClientAssembly);
            domainAssembly.Should().NotReference(commonAssembly);

            infrastructureAssembly.Should().NotReference(applicationAssembly);
            infrastructureAssembly.Should().NotReference(webAPIAssembly);
            infrastructureAssembly.Should().NotReference(webClientAssembly);
            infrastructureAssembly.Should().NotReference(commonAssembly);

            applicationAssembly.Should().NotReference(webAPIAssembly);
            applicationAssembly.Should().NotReference(webClientAssembly);
            applicationAssembly.Should().NotReference(commonAssembly);

            webClientAssembly.Should().NotReference(webAPIAssembly);
        }
    }
}
