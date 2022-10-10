using Microsoft.AspNetCore.Authorization;
using System;

namespace WebShared.Misc.Attributes
{
    [Authorize(Policy = "TeamUser")]
    public class AuthorizeTeamUserAttribute : Attribute
    {

    }
}
