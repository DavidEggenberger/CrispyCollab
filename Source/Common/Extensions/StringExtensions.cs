using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exstensions
{
    public static class StringExtensions
    {
        public static Guid ToGuid(this string stringGuid)
        {
            return new Guid(stringGuid);
        }
    }
}
