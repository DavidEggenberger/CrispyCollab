using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.Team.DTOs
{
    public class InviteTeamMembersDTO
    {
        public List<string> Emails { get; set; }
    }

    public class InviteTeamMembersDTOValidator : AbstractValidator<InviteTeamMembersDTO>
    {
        public InviteTeamMembersDTOValidator()
        {
            RuleForEach(x => x.Emails).EmailAddress();
        }
    }
}
