using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.Team.DTOs
{
    public class InviteMembersDTO
    {
        public List<string> Emails { get; set; }
    }

    public class InviteMembersDTOValidator : AbstractValidator<InviteMembersDTO>
    {
        public InviteMembersDTOValidator()
        {
            RuleForEach(x => x.Emails).EmailAddress();
        }
    }
}
