namespace WebShared.Identity.Team.DTOs
{
    public class InviteMembersDTO
    {
        public List<string> Emails { get; set; }
    }

    //public class InviteMembersDTOValidator : AbstractValidator<InviteMembersDTO>
    //{
    //    public InviteMembersDTOValidator()
    //    {
    //        RuleForEach(x => x.Emails).EmailAddress();
    //    }
    //}
}
