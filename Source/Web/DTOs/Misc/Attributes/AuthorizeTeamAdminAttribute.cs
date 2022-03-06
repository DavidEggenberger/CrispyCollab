using Microsoft.AspNetCore.Authorization;
using System;

namespace Common.Misc.Attributes
{
    [Authorize(Policy = "TeamAdmin")]
    public class AuthorizeTeamAdminAttribute : Attribute
    {

    }
}
