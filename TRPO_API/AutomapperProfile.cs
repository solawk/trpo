using AutoMapper;

namespace TRPO_API
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<TRPO_DM.Models.Element, TRPO_DM.ViewModels.ElementVM>();
            CreateMap<TRPO_DM.Models.Category, TRPO_DM.ViewModels.CategoryVM>();

            CreateMap<TRPO_API.DataModels.ElementDM, TRPO_DM.Models.Element>();
            CreateMap<TRPO_API.DataModels.CategoryDM, TRPO_DM.Models.Category>();
        }
    }
}
