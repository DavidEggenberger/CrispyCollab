namespace Modules.IdentityModule.Shared
{
    public class MemberDTO
    {

        public MembershipStatusDTO Status { get; set; }
        public TeamRoleDTO Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
    }
}
