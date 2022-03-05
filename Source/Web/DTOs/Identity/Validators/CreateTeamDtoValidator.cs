using Common.DTOs.Identity.Team;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.Team.Validators
{
    public class CreateTeamDtoValidator : AbstractValidator<CreateTeamDto>
    {
        public CreateTeamDtoValidator()
        {
            RuleFor(t => t.Name).Must(t => t.Length > 1).WithMessage("The name must be longer than 1 character");
            RuleFor(t => t.Name).NotNull().WithMessage("A name must be set");
        }
    }
}
