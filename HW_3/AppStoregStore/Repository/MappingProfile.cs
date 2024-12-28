using AutoMapper;
using AppStoregStore.Models;
using AppStoregStore.Models.DTO;

namespace AppStoregStore.Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Storage, StorageDto>(MemberList.Destination).ReverseMap();
        }
    }
}
