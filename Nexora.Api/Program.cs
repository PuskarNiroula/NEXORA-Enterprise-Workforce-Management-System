using Microsoft.Extensions.FileProviders;
using NEXORA.Application;
using NEXORA.Controllers;
using NEXORA.Infrastructure;
using NEXORA.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews()
    .AddApplicationPart(typeof(AuthenticationController).Assembly);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var webRoot = Path.GetFullPath(
    Path.Combine(app.Environment.ContentRootPath, "../NEXORA.Web/wwwroot")
);

app.UseStaticFiles();

if (Directory.Exists(webRoot))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(webRoot),
        RequestPath = ""
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

await IdentitySeeder.SeedAsync(app.Services);

app.Run();