using System.Security.Cryptography.X509Certificates;
using Coffee.Application.DependencyInjection;
using Coffee.Infrastructure.DependencyInjection;
using Coffee.Infrastructure.Jobs;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    const string certPath = "/app/cert.pfx";
    options.ListenAnyIP(5001, u => u.UseHttps(new X509Certificate2(certPath, "test1234")));
    options.ListenAnyIP(5000);

});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureApplicationService();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseHangfireDashboard();
HandleWorker.StartRecurringJobs();

app.Run();
