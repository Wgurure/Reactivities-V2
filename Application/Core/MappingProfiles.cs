using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Application.Activities.DTO;
using Application.Activities.DTOs;
using Application.Profiles.DTOs;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
            CreateMap<CreateActivityDto, Activity>();
            CreateMap<EditActivityDto, Activity>();


            CreateMap<Activity, ActivityDto>()
            // ForMember(destinationMember, memberOptions)
            // MapFrom(sourceMember)
            // The HostDisplayname is being configured and how it is being configured is by mapping the first record in the attendees table where the IsHost property is true
            // and mapping that user to their displayname in the users table.
                .ForMember(d => d.HostDisplayName, o => o.MapFrom(s =>  
                s.Attendees.FirstOrDefault(x => x.IsHost)!.User.DisplayName)) // Sets the Host Display name in the ActivityDto as the 
                .ForMember(d => d.HostId, o => o.MapFrom(s =>
                s.Attendees.FirstOrDefault(x => x.IsHost)!.User.Id));
            
            // When mapping activityAttendee to a UserProfile  for the display name we map it form 
            // the Users Display name in the users table that is associated to that user in the ActivityAttendee(FK)
            CreateMap<ActivityAttendee, UserProfile>()
            .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.User.DisplayName))
            .ForMember(d => d.Bio, o => o.MapFrom(s => s.User.Bio))
            .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.User.ImageUrl))
            .ForMember(d => d.Id, o => o.MapFrom(s => s.User.Id));

            CreateMap<User, UserProfile>();
            CreateMap<Comment, CommentDto>()
            .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.User.DisplayName))
            .ForMember(d => d.UserId, o => o.MapFrom(s => s.User.Id))
            .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.User.ImageUrl));

        }
    
    }
}