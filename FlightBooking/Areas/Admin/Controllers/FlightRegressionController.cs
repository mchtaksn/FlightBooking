using FlightBooking.MachineLearningRegressionModel;
using FlightBooking.Services.MachineLearningServices;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FlightRegressionController : Controller
    {
        private readonly FlightRegressionService _flightRegressionService;
        private readonly MongoFlightDataService _flightDataService;
        private readonly FlightMlService _flightMlService;

        public FlightRegressionController(FlightRegressionService flightRegressionService, MongoFlightDataService flightDataService, FlightMlService flightMlService)
        {
            _flightRegressionService = flightRegressionService;
            _flightDataService = flightDataService;
            _flightMlService = flightMlService;
        }

        public async Task<IActionResult> TrainRegressionModel()
        {
            var regressionData = await _flightDataService.ConvertToRegressionDataAsync();

            _flightRegressionService.Train(regressionData);

            ViewBag.Message = "Regression Modeli Başarıyla Eğitildi";
            return View();
        }
        public IActionResult January2027Forecast()
        {
            var result = new List<string>();

            for (int day = 1; day <= 31; day++)
            {
                var date = new DateTime(2027, 1, day);

                // 🌅 Morning
                var morningInput = new FlightRegressionData
                {
                    Month = date.Month,
                    DayOfWeek = (float)date.DayOfWeek,
                    FlightType = 0
                };

                var morningPrediction = _flightRegressionService.Predict(morningInput);

                // 🌙 Evening
                var eveningInput = new FlightRegressionData
                {
                    Month = date.Month,
                    DayOfWeek = (float)date.DayOfWeek,
                    FlightType = 1
                };

                var eveningPrediction = _flightRegressionService.Predict(eveningInput);

                result.Add(
                    $"{date:dd.MM.yyyy} → Morning: {morningPrediction.Score:0} yolcu | Evening: {eveningPrediction.Score:0} yolcu");
            }

            return View(result);

        }
    }
}