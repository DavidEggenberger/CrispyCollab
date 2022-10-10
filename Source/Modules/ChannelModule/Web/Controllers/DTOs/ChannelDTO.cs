using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.ChannelModule.Web.DTOs
{
    public class ChannelDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<MessageDTO> Messages { get; set; }
    }
}
