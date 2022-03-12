using AutoMapper;

namespace TRPO_API
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<TRPO_DM.Models.Element, TRPO_MVC.Models.ElementModel>();
        }
    }
}
