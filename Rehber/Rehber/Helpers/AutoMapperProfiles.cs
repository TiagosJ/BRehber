using AutoMapper;
using Rehber.Dtos;
using Rehber.Models;
using System.Linq;

namespace Rehber.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.firstName,opt => {
                    opt.MapFrom(src => src.User.firstName);
                })
                .ForMember(dest => dest.surName, opt => {
                        opt.MapFrom(src => src.User.surName);
                });

            CreateMap<City, CityForDetailDto>();

            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo,PhotoForReturnDto>();
        }
    }
}
