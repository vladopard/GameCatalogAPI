using AutoMapper;
using GameCatalogAPI.DTOS;
using GameCatalogAPI.Entities;

namespace GameCatalogAPI.Profiles
{
    public class GameProfiles : Profile
    {
        public GameProfiles() 
        {
            CreateMap<Game, GameDTO>()
                .ForMember(dto => dto.DeveloperName,
                ent => ent.MapFrom(g => g.Developer.Name)).ReverseMap();
            CreateMap<CreateGameForDeveloperDTO, Game>();
            CreateMap<GameUpdateDTO, Game>().ReverseMap();
            CreateMap<GameDTO, GameUpdateDTO>()
                .ForMember(gu => gu.ReleaseDate,
                g => g.MapFrom(_ => DateOnly.MinValue));
        }
    }
}
