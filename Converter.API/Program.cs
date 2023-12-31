using Converter.Application.Extensions;
using Converter.Infrastructure.Extensions;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.AspNetCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy =>{
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IDistributedCache>(provider =>
{
    var options = new MemoryDistributedCacheOptions();

    return new MemoryDistributedCache(Options.Create(options));
});


builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage();
});

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapFallbackToController("Index", "Fallback");
app.MapControllers();

app.UseHangfireDashboard("/cdash");



app.Run();

