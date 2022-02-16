﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Identity;
using System.Linq;

namespace WebAPI.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("BFFUser")]
        [AllowAnonymous]
        public ActionResult<BFFUserInfoDTO> GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BFFUserInfoDTO.Anonymous;
            }

            return new BFFUserInfoDTO()
            {
                Claims = User.Claims.Select(claim => new ClaimValueDTO { Type = claim.Type, Value = claim.Value }).ToList()
            };
        }
    }
}
