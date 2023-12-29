using System;

namespace Shared.Modules.Layers.Features.Interfaces
{
    public interface IUserResolver
    {
        Guid GetIdOfLoggedInUser();
    }
}
