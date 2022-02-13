using Infrastructure.Identification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public class IdentificationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

    }
}
