using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{   //https://localhost:7262/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        //Constructor taking various injections
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        //GET ALL REGIONS
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            // Destination first than Source
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }

        //GET SINGLE REGION BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //Create: Action Method
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                //Use Domain Model to create Region
                //CREATE AutoMapperProfile
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //Map Domain model back to DTO
                //GET AutoMapperProfile
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
         

        }

        //Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            
                //Map Dto to Domain Model
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //Pass Dto to repository model
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //We pass the DTO including the ID that wasn't allowed to be edited by the client
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
            
        }

        //Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }


            //If you want to send it back
            //Map Domain Model to DTO

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto.Name + " was deleted");

        }

    }
}






//Old stuff from GetALl

//var regionsDto = new List<RegionDto>();

//foreach (var region in regionsDomain)
//{
//    regionsDto.Add(new RegionDto()
//    {
//        Id = region.Id,
//        Code = region.Code,
//        Name = region.Name,
//        RegionImageUrl = region.RegionImageUrl,
//    });
//}

// +++++++++++++++++++++


//comment ctrl+k, ctrl+c
//To uncomment ctrl+k, ctrl+u


//var regions = new List<Region>
//            {
//                new Region
//                {
//                    Id = Guid.NewGuid(),
//                    Name = "AuckLand Region",
//                    Code = "AKL",
//                    RegionImageUrl = "https://images.pexels.com/photos/17060538/pexels-photo-17060538/free-photo-of-clear-sky-over-waterfall.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
//                },
//                new Region
//                {
//                    Id = Guid.NewGuid(),
//                    Name = "Wellington Region",
//                    Code = "WLG",
//                    RegionImageUrl = "https://images.pexels.com/photos/17079172/pexels-photo-17079172/free-photo-of-moss-on-rocks-on-shore.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
//                },
//            };