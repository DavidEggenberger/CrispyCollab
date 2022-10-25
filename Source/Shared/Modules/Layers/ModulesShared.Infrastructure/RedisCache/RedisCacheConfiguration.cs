using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Modules.Layers.Infrastructure.RedisCache
{
    public class RedisCacheConfiguration
    {
        public string Password { get; set; }
        public string ConnectionString { get; set; }
    }
}
