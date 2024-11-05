namespace WebShared.Identity.Team.AdminManagement
{
    public class RevokeInvitationDTO
    {
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
    }
}
