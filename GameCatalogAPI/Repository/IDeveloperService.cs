using GameCatalogAPI.DTOS;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameCatalogAPI.Repository
{
    public interface IDeveloperService
    {
        Task<DeveloperDTO?> CreateAsync(CreateDeveloperDTO newDevDTO);
        Task<IEnumerable<DeveloperDTO>> GetAllAsync();
        Task<DeveloperDTO?> GetSingleDevAsync(int id);
        Task<bool> PatchAsync(int id, DeveloperUpdateDTO patchedDTO);
    }
}