namespace Modules.IdentityModule.Web.DTOs
{
    public class TeamDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }

    //public class TeamDTOValidator : AbstractValidator<TeamDTO>
    //{
    //    public TeamDTOValidator()
    //    {
    //        RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be set");
    //        RuleFor(x => x.Name).MinimumLength(2).WithMessage("Name must be between 2 and 10 characters").MaximumLength(10).WithMessage("Name must be between 2 and 10 characters");
    //    }
    //}
}
