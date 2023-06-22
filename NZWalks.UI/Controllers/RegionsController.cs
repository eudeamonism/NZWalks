﻿using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO;
//This will render API
namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {

            List<RegionDto> response = new List<RegionDto>();

            try
            {
                //Get All Regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7262/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                //extract body
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

            }
            catch (Exception ex)
            {
                //Log exception
                throw;
            }

            return View(response);
        }
    }
}


//a DTO was created with the same stuff. Because theoritically you wouldn't have two apis ina project.
