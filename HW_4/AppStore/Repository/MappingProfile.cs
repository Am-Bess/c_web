using AutoMapper;
using AppStore.Models;
using AppStore.Models.DTO;

namespace AppStore.Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Storage, StorageDto>(MemberList.Destination).ReverseMap();
        }
    }
}
