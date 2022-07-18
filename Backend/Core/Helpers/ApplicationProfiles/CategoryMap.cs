using AutoMapper;
using Core.DTO.Category;
using Core.Entities;

namespace Core.Helpers.ApplicationProfiles
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<CreateCategoryDTO, Category>();
        }
    }
}
