using GroceryPridictor.ML.Models;
using Microsoft.ML;
using System.IO;
using System.Linq;

namespace GroceryPridictor.ML
{
    public class RegionPrediction
    {
        static readonly string _dataPath = Path.Combine("wwwroot", "Data", "LONGS_LATS_DATA.csv");
        static readonly string _modelPath = Path.Combine("wwwroot", "Data", "RegionsClusteringModel.zip");
        private readonly MLContext _context;
        private PredictionEngine<LatLongModel, RegionPredictionModel> predictor;
        public RegionPrediction()
        {
            _context = new MLContext(0);
        }

        public void GenerateModel()
        {
            IDataView dataView = _context.Data.LoadFromTextFile<LatLongModel>(_dataPath, hasHeader: false, separatorChar: ',');

            //pipeline
            string featuresColumnName = "Features";
            var pipeline = _context.Transforms
                .Concatenate(featuresColumnName, "Latitude", "Longitude")
                .Append(_context.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 5));

            //train
            var model = pipeline.Fit(dataView);
            //save
            using (var fileStream = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                _context.Model.Save(model, dataView.Schema, fileStream);
            }

            predictor = _context.Model.CreatePredictionEngine<LatLongModel, RegionPredictionModel>(model);
        }

        public RegionPredictionModel GetRegion(LatLongModel model)
        {
            if (predictor is null)
            {
                if (File.Exists(_modelPath))
                {
                    // Load trained model
                    ITransformer trainedModel = _context.Model.Load(_modelPath, out _);
                    predictor = _context.Model.CreatePredictionEngine<LatLongModel, RegionPredictionModel>(trainedModel);
                }
                else
                {
                    GenerateModel();
                }
            }
            var prediction = predictor.Predict(model);
            return prediction;
        }
    }
}
