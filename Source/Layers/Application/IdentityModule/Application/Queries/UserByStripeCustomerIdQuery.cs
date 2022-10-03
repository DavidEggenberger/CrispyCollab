using Infrastructure.CQRS.Query;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Identity.Queries
{
    public class UserByStripeCustomerIdQuery : IQuery<ApplicationUser>
    {
        public string StripeCustomerId { get; set; }
    }
}
