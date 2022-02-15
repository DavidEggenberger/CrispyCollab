using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Identity
{
    public class BFFUserInfoDTO
    {
        public static readonly BFFUserInfoDTO Anonymous = new BFFUserInfoDTO();
        public string NameClaimType { get; set; }
        public List<ClaimValueDTO> Claims { get; set; }
    }
}
