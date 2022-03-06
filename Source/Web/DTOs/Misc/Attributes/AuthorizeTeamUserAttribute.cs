using Microsoft.AspNetCore.Authorization;
using System;

namespace Common.Misc.Attributes
{
    [Authorize(Policy = "TeamUser")]
    public class AuthorizeTeamUserAttribute : Attribute
    {

    }
}
