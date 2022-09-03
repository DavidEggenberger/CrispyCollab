﻿using Application.TenantAggregate.Queries;
using AutoMapper;
using Domain.Aggregates.TenantAggregate;
using Infrastructure.CQRS.Query;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Identity;
using WebShared.DTOs.Aggregates.Tenant;

namespace WebServer.Infrastructure.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IMapper mapper;
        private readonly IQueryDispatcher queryDispatcher;
        public ApplicationUserController(SignInManager<ApplicationUser> signInManager, ApplicationUserManager applicationUserManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.applicationUserManager = applicationUserManager;
            this.mapper = mapper;
        }

        [HttpGet]
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

        [HttpGet("selectedTeam")]
        public async Task<TenantDTO> GetSelectedTeamForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);

            var tenantByIdQuery = new GetTenantByQuery() { TenantId = applicationUser.SelectedTenantId };
            Tenant currentTenant = await queryDispatcher.DispatchAsync<GetTenantByQuery, Tenant>(tenantByIdQuery);

            return mapper.Map<TenantDTO>(currentTenant);
        }
    }
}