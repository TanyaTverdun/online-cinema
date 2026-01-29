using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

using onlineCinema.Infrastructure.Data;
using onlineCinema.Infrastructure.Repositories;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Mapping;
using onlineCinema.Validators;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        connectionString,
        b => b.MigrationsAssembly("onlineCinema.Infrastructure")
    );
    options.LogTo(Console.WriteLine, LogLevel.Information);
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<MovieScheduleViewModelMapper>();
builder.Services.AddSingleton<SessionMapper>();
builder.Services.AddSingleton<PaymentMapper>();
builder.Services.AddSingleton<BookingMapper>();
builder.Services.AddSingleton<SnackMapper>();
builder.Services.AddSingleton<BookingViewModelMapper>();
builder.Services.AddSingleton<SnackViewModelMapper>();
builder.Services.AddSingleton<HallMapper>();
builder.Services.AddSingleton<HallViewModelMapper>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ISnackService, SnackService>();
builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddValidatorsFromAssemblyContaining<BookingInputViewModelValidator>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        await DbInitializer.Initialize(context, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Сталася помилка під час заповнення БД.");
    }
}

var supportedCultures = new[] { new CultureInfo("uk-UA") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("uk-UA"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
