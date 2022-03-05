using Microsoft.AspNetCore.Authorization;
using System;

namespace WebServer.Framwork.Attributes
{
    [Authorize(Policy = "TeamAdmin")]
    public class AuthorizeTeamAdminAttribute : Attribute
    {

    }
}
