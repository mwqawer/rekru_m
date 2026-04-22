using Millenium_rekru.Middlewares;
using Millenium_rekru.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IDataProcessingService, DataProcessingService>();
// app.UseHttpsRedirection();



app.MapControllers();
app.UseMiddleware<RequestCounterMiddleware>();
app.Run();
