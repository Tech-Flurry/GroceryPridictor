using Microsoft.ML.Data;

namespace GroceryPridictor.ML.Models
{
    public class LatLongModel
    {
        [LoadColumn(0)]
        public float Latitude { get; set; }
        [LoadColumn(1)]
        public float Longitude { get; set; }
    }
    public class RegionPredictionModel
    {
        [ColumnName("PredictedLabel")]
        public uint RegionId { get; set; }
        [ColumnName("Score")]
        public float[] Distances { get; set; }
    }
}
