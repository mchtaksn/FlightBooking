using FlightBooking.Dtos.CheckInDtos;
using FlightBooking.Services.BookingServices;
using FlightBooking.Services.CheckInServices;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CheckInController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ICheckInService _chechInService;

        public CheckInController(IBookingService bookingService, ICheckInService chechInService)
        {
            _bookingService = bookingService;
            _chechInService = chechInService;
        }

        //public async Task< IActionResult> Index(string id)
        //{
        //    ViewBag.FlightNumber = TempData.Peek("flightnumber");
        //    ViewBag.DepartureTime = TempData.Peek("DepartureTime");
        //    ViewBag.ArrivalTime = TempData.Peek("ArrivalTime");

        //    var passenger = await _bookingService.GetPassengerNameByIdAsync(id);
        //    var pnrNumber = await _bookingService.GetPnrByPassengerIdAsync(id);
        //    var gate = await _bookingService.GetGateByPassengerIdAsync(id);

        //    ViewBag.Gate = gate;
        //    ViewBag.PnrNumber = pnrNumber;
        //    ViewBag.Name =passenger.Name;
        //    ViewBag.Surname =passenger.Surname;

        //    return View();
        //}


        public async Task<IActionResult> Index(string id)
        {
            ViewBag.FlightNumber = TempData["FlightNumber"];
            ViewBag.DepartureTime = TempData["DepartureTime"];
            ViewBag.ArrivalTime = TempData["ArrivalTime"];
            ViewBag.AirlineCode = TempData["AirlineCode"];          // banner'da kullanılıyor
            ViewBag.DepartureAirportCode = TempData["DepartureAirportCode"];
            ViewBag.DepartureAirportName = TempData["DepartureAirportName"];
            ViewBag.ArrivalAirportCode = TempData["ArrivalAirportCode"];
            ViewBag.ArrivalAirportName = TempData["ArrivalAirportName"];
            ViewBag.BasePrice = TempData["BasePrice"];
            ViewBag.Currency = TempData["Currency"];

            var passenger = await _bookingService.GetPassengerNameByIdAsync(id);
            var pnrNumber = await _bookingService.GetPnrByPassengerIdAsync(id);
            var gate = await _bookingService.GetGateByPassengerIdAsync(id);
            //var flightId = await _bookingService.GetFlightIdByPassengerIdAsync(id); // 🔥 yeni metod

            ViewBag.Name = passenger.Name;
            ViewBag.Surname = passenger.Surname;
            ViewBag.PassengerName = passenger.Name + " " + passenger.Surname;
            ViewBag.PnrNumber = pnrNumber;
            ViewBag.Pnr = pnrNumber;   // modal'da @ViewBag.Pnr kullanılıyor
            ViewBag.Gate = gate;

            // 🔥 Form için gerekli — hidden field olarak view'a taşınacak
            ViewBag.PassengerId = id;
            ViewBag.FlightId = "6a7b6642e034213b59d5641a";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CompleteCheckInDto completeCheckInDto)
        {
            await _chechInService.CompleteCheckInAsync(completeCheckInDto);
            return RedirectToAction("Test");
        }
    }
}
