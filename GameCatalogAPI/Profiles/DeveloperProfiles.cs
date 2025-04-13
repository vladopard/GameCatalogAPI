using AutoMapper;
using GameCatalogAPI.DTOS;
using GameCatalogAPI.Entities;

namespace GameCatalogAPI.Profiles
{
    public class DeveloperProfiles : Profile
    {
        public DeveloperProfiles() 
        {
            CreateMap<Developer, DeveloperDTO>();
            CreateMap<CreateDeveloperDTO, Developer>();
        }
    }
}
