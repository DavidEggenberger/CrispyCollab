using Microsoft.AspNetCore.Authorization;
using System;

namespace WebServer.Framwork.Attributes
{
    [Authorize(Policy = "TeamUser")]
    public class AuthorizeTeamUserAttribute : Attribute
    {

    }
}
