using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace InsureYouAI.Services
{
    public class PolicySalesData
    {
        public DateTime Date { get; set; }      //makineye verdiğimiz veri :  Ocak  → 120 satış,Şubat → 135 satış gibi
        public float SaleCount { get; set; }
    }
    public class PolicySalesForecast
    {
        public float[] ForecastedValues { get; set; }       //AI’ın bize döndüğü sonuç
        public float[] LowerBoundValues { get; set; }       //Gelecek 3 ay: Tahmin:        160, 170, 180, En kötü ihtimal:150, 158, 165...
        public float[] UpperBoundValues { get; set; }      


     
    }
    public class ForecastService
    {
        private readonly MLContext _mlContext;
        public ForecastService()
        {
            _mlContext = new MLContext();
        }

        public PolicySalesForecast GetForecast(List<PolicySalesData> salesData, int horizon = 3)
        {

            int count = salesData.Count;

            var dataView = _mlContext.Data.LoadFromEnumerable(salesData);
            var forecastingPipeline = _mlContext.Forecasting.ForecastBySsa(
                outputColumnName: "ForecastedValues",
                inputColumnName: "SaleCount",
                windowSize: Math.Max(2, count / 4),          // toplamın 1/4'ü
                seriesLength: Math.Max(4, count / 2),        // toplamın yarısı
                trainSize: count - horizon,
                horizon: horizon,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: "LowerBoundValues",
                confidenceUpperBoundColumn: "UpperBoundValues"
                );

            var model = forecastingPipeline.Fit(dataView);
            var forecastingEngine = model.CreateTimeSeriesEngine<PolicySalesData, PolicySalesForecast>(_mlContext);

            return forecastingEngine.Predict();
        }

    }
}