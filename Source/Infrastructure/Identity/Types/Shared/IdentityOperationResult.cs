using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Types.Shared
{
    public class IdentityOperationResult
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public static IdentityOperationResult Success() => new IdentityOperationResult { Successful = true };
        public static IdentityOperationResult Fail(string message) => new IdentityOperationResult { Successful = false, Message = message };
    }
}
