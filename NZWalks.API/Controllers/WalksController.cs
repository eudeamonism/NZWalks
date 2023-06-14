using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        private readonly WalkDto walkDto;


        //Constructor is used for injections
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        //Create Walk, POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            //Call Repository to create a new walk
            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);


            //Map DomainModel to Dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        //Get All Walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            //Map Domain Model DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        //Get Walk By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Update a Walk by Id
        //We're expecting DTO
        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //Destination, Source
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            //Persist to DB
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            //Check if null
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //Return as DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var walkDomainModel = await walkRepository.DeleteAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound(); 
            }

           var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto.Name + " was deleted");

        }
    }
}


//Note: When you finally send back the data as a DTO, don't forget that yo need a DTO for that.
//WalkDto is the actual DTO we created, source, and it stems from our model fresh from the repository, walkDomainModel