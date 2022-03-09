using Infrastructure.Identity;
using Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Infrastructure.Identity
{
    public class TeamTests
    {
        private Team team = new Team(new ApplicationUser
        {
            UserName = "TestUserName"
        }, new Subscription(), "TestTeam");
        public void ShouldFailWhenAddingUserWhoIsAlreadyMember()
        {
            
        }
    }
}
