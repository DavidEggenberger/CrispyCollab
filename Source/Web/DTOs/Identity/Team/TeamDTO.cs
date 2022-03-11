using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.DTOs.TeamDTOs
{
    public class TeamDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }

    public class TeamDTOValidator : AbstractValidator<TeamDTO>
    {
        public TeamDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be set");
            RuleFor(x => x.Name).MinimumLength(2).MaximumLength(10).WithMessage("Name must be between 2 and 10 characters");
        }
    }
}
