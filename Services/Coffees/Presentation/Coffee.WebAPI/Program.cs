using System.Security.Cryptography.X509Certificates;
using Coffee.Application.DependencyInjection;
using Coffee.Infrastructure.DependencyInjection;
using Coffee.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    const string certPath = "/app/cert.pfx";
    options.ListenAnyIP(5081, u => u.UseHttps(new X509Certificate2(certPath, "test1234")));
    options.ListenAnyIP(5080);
});

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureApplicationService();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigureSwaggerOptions(builder.Configuration);

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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();