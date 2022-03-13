using AutoMapper;
using Common.Identity.DTOs.TeamDTOs;
using Common.Identity.Subscription;
using Common.Identity.Team;
using Common.Identity.Team.AdminManagement;
using Common.Identity.User;
using Infrastructure.Identity;
using Infrastructure.Identity.BusinessObjects;
using Infrastructure.Identity.Entities;
using System.Linq;

namespace WebServer.Mappings
{
    public class IdentityProfiles : Profile
    {
        public IdentityProfiles()
        {
            CreateMap<TeamMetrics, TeamMetricsDTO>();
            CreateMap<ApplicationUserTeam, MemberDTO>()
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role));
            CreateMap<Team, TeamDTO>();
            CreateMap<Subscription, SubscriptionDTO>()
                .ForMember(x => x.SubscriptionPlanType, opt => opt.MapFrom(opt => opt.SubscriptionPlan.PlanType))
                .ForMember(x => x.PeriodEnd, opt => opt.MapFrom(opt => opt.PeriodEnd))
                .ForMember(x => x.SubscriptionStatus, opt => opt.MapFrom(opt => opt.Status));
            CreateMap<Team, TeamAdminInfoDTO>()
                .ForMember(x => x.AdminNotifications, opt => opt.MapFrom(x => x.Notifications));
            CreateMap<SubscriptionPlan, SubscriptionPlanDTO>();
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<AdminNotification, AdminNotificationDTO>()
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(opt => opt.CreatedAt));
        }
    }
}
