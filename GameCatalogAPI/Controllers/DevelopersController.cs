using System.Xml.XPath;
using AutoMapper;
using GameCatalogAPI.DTOS;
using GameCatalogAPI.Entities;
using GameCatalogAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace GameCatalogAPI.Controllers
{
    [Route("api/developers")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperDTO>>> GetAllDevelopers()
        {
            return Ok(await _developerService.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetSingleDeveloper")]
        public async Task<ActionResult<DeveloperDTO>> GetSingleDevelopers(int id)
        {
            var dev = await _developerService.GetSingleDevAsync(id);
            if (dev == null) return NotFound();
            return Ok(dev);
        }

        [HttpPost]
        public async Task<ActionResult<DeveloperDTO>> CreateDeveloper(
            CreateDeveloperDTO newDevDTO)
        {
            if (newDevDTO == null)
                return BadRequest("Developer information is missing.");

            var result = await _developerService.CreateAsync(newDevDTO);
            if(result == null)
                return BadRequest("Could not create developer.");

            return CreatedAtRoute("GetSingleDeveloper",
                new { id = result.Id },
                result);

        }

        [HttpPatch("id")]
        public async Task<ActionResult> PartiallyUpdateDeveloper(int id,
            JsonPatchDocument<DeveloperUpdateDTO> patchDocument)
        {
            if (patchDocument == null) return BadRequest();

            var currentEntity = await _developerService.GetSingleDevAsync(id);
            if (currentEntity == null) return NotFound();

            var patchingDTO = new DeveloperUpdateDTO
            {
                Name = currentEntity.Name,
                Country = currentEntity.Country,
                Founded = currentEntity.Founded
            };

            patchDocument.ApplyTo(patchingDTO, ModelState);

            if(!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var success = await _developerService.PatchAsync(id, patchingDTO);
            return success ? NoContent() : NotFound();
        }
    }
}
