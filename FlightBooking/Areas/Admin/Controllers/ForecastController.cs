using FlightBooking.MachineLearningModels;
using FlightBooking.Services.MachineLearningServices;
using FlightBooking.Services.NoShowServices;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ForecastController : Controller
    {
        private readonly MongoFlightDataService _flightDataService;
        private readonly FlightMlService _flightMlService;
        private readonly NoShowService _noShowService;

        public ForecastController(MongoFlightDataService flightDataService, FlightMlService flightMlService, NoShowService noShowService)
        {
            _flightDataService = flightDataService;
            _flightMlService = flightMlService;
            _noShowService = noShowService;
        }

        [HttpGet]
        public async Task<IActionResult> NoShowAnalysis()
        {
            var values = await _noShowService.GetSlotBasedNoShowRatesAsync();
            return View(values);
        }
        public async Task< IActionResult> TrainModel()
        {
            var mlData =  await _flightDataService.ConvertToMlDataAsync();
            _flightMlService.Train(mlData);
            ViewBag.Message = "Model Başarıyla Eğitildi";
            return View();
        }
        public IActionResult Predict()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Predict(DateTime flightDate, string flightType)
        {
            var input = new FlightData
            {
                Month = flightDate.Month,

                DayOfWeek = (float)flightDate.DayOfWeek,

                FlightType = flightType == "Morning" ? 0 : 1
            };

            var prediction = _flightMlService.Predict(input);

            ViewBag.Result = prediction.PredictedLabel
                ? "Bu uçuş büyük ihtimal dolacaktır."
                : "Bu uçuşta yoğunluk düşük görünüyor.";

            ViewBag.Probability = prediction.Probability;

            return View();
        }

    }
}
