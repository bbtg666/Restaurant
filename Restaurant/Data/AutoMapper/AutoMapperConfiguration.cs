using AutoMapper;
using Core.Models.Requests;
using Data.Entities;

namespace Data.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<MealRequest, Meal>();
            CreateMap<MealRequest, Meal>().ReverseMap();
            CreateMap<MealCategoryRequest, MealCategory>();
            CreateMap<MealCategoryRequest, MealCategory>().ReverseMap();
        }
    }
}
