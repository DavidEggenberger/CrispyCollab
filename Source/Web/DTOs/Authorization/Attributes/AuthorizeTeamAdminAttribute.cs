using Microsoft.AspNetCore.Authorization;
using System;

namespace WebShared.Misc.Attributes
{
    [Authorize(Policy = "TeamAdmin")]
    public class AuthorizeTeamAdminAttribute : Attribute
    {

    }
}
