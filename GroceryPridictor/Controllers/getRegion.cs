using GroceryPridictor.ML;
using GroceryPridictor.ML.Models;

namespace GroceryPridictor.Controllers
{
    public class getRegion
    {
        public static int getRegionFun(string latitude, string longitude)
        {
            var regionPredictor = new RegionPrediction();
            var prediction = regionPredictor.GetRegion(new LatLongModel
            {
                Latitude = float.Parse(latitude),
                Longitude = float.Parse(longitude)
            });
            int region = (int)prediction.RegionId;
            return region;
        }

    }
}
