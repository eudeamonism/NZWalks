﻿namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        //This can have null values because of ?
        public string? RegionImageUrl { get; set; }  
    }
}
