using AkilliSayac.Services.Report.Dtos;
using AutoMapper;

namespace AkilliSayac.Services.Report.Mapping
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Report, ReportDto>().ReverseMap();
        }
       
    }
}
