using System.Reflection.Metadata;
using AutoMapper;
using GameCatalogAPI.DTOS;
using GameCatalogAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameCatalogAPI.Repository
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public DeveloperService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeveloperDTO>> GetAllAsync()
        {
            var developers = await _gameRepository.GetAllDevelopersAsync();
            return _mapper.Map<IEnumerable<DeveloperDTO>>(developers);
        }

        public async Task<DeveloperDTO?> GetSingleDevAsync(int id)
        {
            var devEntity = await _gameRepository.GetDeveloperAsync(id);
            return devEntity == null ? null : _mapper.Map<DeveloperDTO>(devEntity);
        }

        public async Task<DeveloperDTO?> CreateAsync(CreateDeveloperDTO newDevDTO)
        {
            var newDevEntity = _mapper.Map<Developer>(newDevDTO);
            var addedDevEntity = await _gameRepository.AddDeveloperAsync(newDevEntity);

            return addedDevEntity == null ? null : _mapper.Map<DeveloperDTO>(addedDevEntity);
        }

        public async Task<bool> PatchAsync(int id,
            DeveloperUpdateDTO patchedDTO)
        {
            //EF CORE VERSION
            //var devEntity = await _gameRepository.GetDeveloperAsync(id);
            //_mapper.Map(patchedDTO, devEntity);
            //return await _gameRepository.SaveChangesAsync();

            var updatedEntity = _mapper.Map<Developer>(patchedDTO);
            updatedEntity.Id = id;
            return await _gameRepository.UpdateDeveloperAsync(updatedEntity);
        }
    }
}



//[Controller Layer]
//    ↓ Validates model using ModelState
//    ↓
//[Service Layer]
//Handles logic, DB access, mapping, etc.