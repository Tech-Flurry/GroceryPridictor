using GroceryPridictor.ML;
using GroceryPridictor.ML.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPridictor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        [HttpGet("Region")]
        public IActionResult GetRegion(decimal lat, decimal lng)
        {
            var regionPredictor = new RegionPrediction();
            return Ok(regionPredictor.GetRegion(new LatLongModel
            {
                Latitude = float.Parse(lat.ToString()),
                Longitude = float.Parse(lng.ToString())
            }));
        }
    }
}
