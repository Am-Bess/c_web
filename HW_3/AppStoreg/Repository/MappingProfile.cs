using AutoMapper;
using AppStoreg.Models;
using AppStoreg.Models.DTO;

namespace AppStoreg.Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDto>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryDto>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageDto>(MemberList.Destination).ReverseMap(); 
        }
    }
}
