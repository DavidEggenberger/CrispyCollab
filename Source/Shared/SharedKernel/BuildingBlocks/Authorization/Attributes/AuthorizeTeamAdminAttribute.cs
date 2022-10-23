using Microsoft.AspNetCore.Authorization;

namespace WebShared.Misc.Attributes
{
    [Authorize(Policy = "TeamAdmin")]
    public class AuthorizeTeamAdminAttribute : Attribute
    {

    }
}
