using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using NEXORA.Application;
using NEXORA.Controllers;
using NEXORA.Infrastructure;
using NEXORA.Infrastructure.Data;
using NEXORA.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews()
    .AddApplicationPart(typeof(AuthenticationController).Assembly);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddIdentity<UserDbModel, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/login";
    options.Cookie.Name = "NEXORA.Auth";
});

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