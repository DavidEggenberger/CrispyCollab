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
            
        }
    }
}
