using System.Security.Cryptography.X509Certificates;
using Identity.Application.DependencyInjection;
using Identity.Infrastructure.DependencyInjection;

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
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .WithOrigins("http://localhost:3000")                        
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    }));   


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();