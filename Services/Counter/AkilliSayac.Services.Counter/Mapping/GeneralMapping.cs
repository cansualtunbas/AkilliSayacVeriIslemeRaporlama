using AkilliSayac.Services.Counter.Dtos;
using AutoMapper;


namespace AkilliSayac.Services.Counter.Mapping
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Counter, CounterDto>().ReverseMap();
        }
    }
}
