using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Modules.Layers.Infrastructure.Interfaces
{
    public interface IUserResolver
    {
        Guid GetIdOfLoggedInUser();
    }
}
