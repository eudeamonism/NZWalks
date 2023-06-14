﻿namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        
        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}

//This doesn't get id, because this is the object that the client is sending us. 