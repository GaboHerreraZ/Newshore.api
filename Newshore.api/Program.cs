

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newshore.api.Business;
using Newshore.api.Context;
using Newshore.api.Integration;
using Newshore.api.Mapper;
using Newshore.api.Middleware;
using Newshore.api.Model;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(Program), typeof(MapperConfig));
builder.Services.AddSingleton(typeof(IHttpClientService<>), typeof(HttpClientService<>));
builder.Services.AddScoped<IRouteService, RouteService>();

builder.Services.AddDbContext<RouteDb>(opt => opt.UseInMemoryDatabase("RouteList"));

builder.Logging.ClearProviders();


builder.Logging.AddSerilog(new LoggerConfiguration()
    .WriteTo.File("D:\\docs\\log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger());


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RouteDb>();
    var dataService = scope.ServiceProvider.GetRequiredService<IRouteService>();

    await dataService.LoadRoutesAsync();
}



app.UseMiddleware<ExceptionHandlerMiddleware>();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/routes/{origin}/{destination}", async (IRouteService routeService, string origin, string destination ) =>
{
    List<Journey> routes = await routeService.FindAllRoutes(origin, destination);
    return routes;
})
.WithOpenApi();

app.UseCors();

app.Run();
