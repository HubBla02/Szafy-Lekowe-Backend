using AutoMapper;
using SzafyNaLeki.Entities;
using SzafyNaLeki.Models;

namespace SzafyNaLeki
{
    public class SzafaMappingProfile : Profile
    {
        public SzafaMappingProfile() 
        {
            CreateMap<Szafa, SzafaDto>()
                .ForMember(x => x.Temperatura1, x => x.MapFrom(x => x.Temperatura1))
                .ForMember(x => x.Temperatura2, x => x.MapFrom(x => x.Temperatura2))
                .ForMember(x => x.Alarm, x => x.MapFrom(x => x.Alarm));

            CreateMap<UtworzSzafeDto, Szafa>()
                .ForMember(x => x.Temperatura1, x => x.MapFrom(x => x.Temperatura1))
                .ForMember(x => x.Temperatura2, x => x.MapFrom(x => x.Temperatura2))
                .ForMember(x => x.Alarm, x => x.MapFrom(x => x.Alarm));
        }

    }
}
