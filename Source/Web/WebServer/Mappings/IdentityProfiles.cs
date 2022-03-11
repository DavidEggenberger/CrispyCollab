using AutoMapper;
using Common.Identity.DTOs.TeamDTOs;
using Infrastructure.Identity;
using System.Linq;

namespace WebServer.Mappings
{
    public class IdentityProfiles : Profile
    {
        public IdentityProfiles()
        {
            CreateMap<Team, TeamDTO>();
            CreateMap<Team, TeamAdminInfoDTO>()
                .ForMember(x => x.SubscriptionStatus, opt => opt.MapFrom(x => x.Subscription.Status))
                .ForMember(x => x.SubscriptionPlanType, opt => opt.MapFrom(x => x.Subscription.SubscriptionPlan.PlanType))
                .ForMember(x => x.Members, opt => opt.MapFrom(x => x.Members));
        }
    }
}
