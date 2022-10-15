using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.ChannelModule.Web.DTOs.Commands
{
    public class CreateChannelDTO
    {
        public string Name { get; set; }
        public string Goal { get; set; }
    }
    //public class CreateChannelDTOValidator : AbstractValidator<CreateChannelDTO>
    //{
    //    public CreateChannelDTOValidator()
    //    {
    //        RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be set");
    //        RuleFor(x => x.Name).MinimumLength(2).WithMessage("Name must be between 2 and 10 characters").MaximumLength(10).WithMessage("Name must be between 2 and 10 characters");
    //    }
    //}
}
