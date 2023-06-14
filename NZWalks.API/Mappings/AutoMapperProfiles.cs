using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //Source Model to Destination Model

            //GET Region
            CreateMap<Region, RegionDto>().ReverseMap();

            //Create Region
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            //Region, Update: Dto => Domain => Dto. We need to use same body variable in Update
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();

            CreateMap<Walk, WalkDto>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto >().ReverseMap();

            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();

        }
    }
}

//NOTES: For CREATE controller, we first used the Create Profile to convert DTO to Domain. This then was persisted to database
//via respoistory. We then used GET profile because it has RegionDto, and this is the object we want in our response