using AutoMapper;
using EventManagement.Core.DTOs;
using EventManagement.Core.Models;
using EventManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.InfraStructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Event, EventDTO>()
                .ForMember(dest => dest.SpeakerName, opt => opt.MapFrom(src => src.SpeakerId.ToString()))
                .ReverseMap();

            
            CreateMap<User, UserDTO>().ReverseMap();

         
            CreateMap<Speaker, SpeakerDTO>().ReverseMap();

            
            CreateMap<Registration, RegistrationDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event.Title))
                .ReverseMap();
        }
    }
}
