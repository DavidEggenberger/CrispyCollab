using AutoMapper;
using WebShared.Identity.Subscription;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly SubscriptionManager subscriptionManager;
        private readonly IMapper mapper;
        public SubscriptionPlanController(SubscriptionManager subscriptionManager, IMapper mapper)
        {
            this.subscriptionManager = subscriptionManager;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<SubscriptionPlanDTO> GetSubscriptionPlanById(Guid id)
        {
            SubscriptionPlan subscriptionPlan = await subscriptionManager.FindByIdAsync(id);
            return mapper.Map<SubscriptionPlanDTO>(subscriptionPlan);
        }

        [HttpGet("all")]
        public async Task<List<SubscriptionPlan>> GetAllSubscriptionPlan()
        {
            List<SubscriptionPlan> subscriptionPlans = subscriptionManager.GetAllSubscriptionPlans();
            return mapper.Map<List<SubscriptionPlan>>(subscriptionPlans);
        }
    }
}
