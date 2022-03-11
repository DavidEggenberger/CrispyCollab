using AutoMapper;
using Common.Identity.DTOs.TeamDTOs;
using Common.Identity.Team;
using Infrastructure.Identity;
using System.Linq;

namespace WebServer.Mappings
{
    public class IdentityProfiles : Profile
    {
        public IdentityProfiles()
        {
            
            CreateMap<ApplicationUserTeam, MemberDTO>()
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role));
            CreateMap<Team, TeamDTO>();
            CreateMap<Team, TeamAdminInfoDTO>()
                .ForPath(x => x.Subscription.SubscriptionStatus, opt => opt.MapFrom(x => x.Subscription.Status))
                .ForPath(x => x.Subscription.SubscriptionPlanType, opt => opt.MapFrom(x => x.Subscription.SubscriptionPlan.PlanType))
                .ForMember(x => x.Members, opt => opt.MapFrom(x => x.Members));
        }
    }
}
