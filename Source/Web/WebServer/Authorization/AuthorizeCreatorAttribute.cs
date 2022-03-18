using Microsoft.AspNetCore.Authorization;
using System;

namespace WebServer.Authorization
{
    [Authorize(Policy = "CreatorPolicy")]
    public class AuthorizeCreatorAttribute : Attribute
    {

    }
}
