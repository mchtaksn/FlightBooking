using FlightBooking.Services.BookingServices;
using FlightBooking.Services.FlightServices;
using FlightBooking.Settings;
using Microsoft.Extensions.Options;
using System.Reflection;
using MongoDB.Driver;
using FlightBooking.Entities;
using FlightBooking.Services.CheckInServices;
using FlightBooking.Services.MachineLearningServices;
using FlightBooking.Services.NoShowServices;
using FlightBooking.Services.OverBookingNoShowServices;
using FlightBooking.AgentServices;
using FlightBooking.AgentServices.OpenAIServices;
using FlightBooking.AgentSettings;
using FlightBooking.AgentServices.PromptBuilders;
using FlightBooking.AgentServices.IntentDetectors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFlightService,FlightService>();
builder.Services.AddScoped<IBookingService,BookingService>();
builder.Services.AddScoped<ICheckInService,CheckInService>();
builder.Services.AddSingleton<FlightRegressionService>();
builder.Services.AddScoped<NoShowService>();
builder.Services.AddScoped<OverbookingRecommendationService>();
builder.Services.AddScoped<NoShowPredictionService>();
builder.Services.AddScoped<ITravelAgentService,TravelAgentService>();
builder.Services.AddScoped<IOpenAIService,OpenAIService>();
builder.Services.AddScoped<ITravelPromptBuilder,TravelPromptBuilder>();
builder.Services.AddScoped<IIntentDetector,TravelIntentDetector>();
builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAI"));
builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<FlightMlService>();
builder.Services.AddScoped<MongoFlightDataService>();


// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingsKey"));
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddScoped<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IDatabaseSettings>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IDatabaseSettings>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Booking>("Bookings"));

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Flight>("Flights"));

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<CheckIn>("CheckIns"));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
