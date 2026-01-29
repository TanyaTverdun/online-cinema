using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Infrastructure.Data;
using onlineCinema.Infrastructure.Repositories;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Application.Services;
using onlineCinema.Mapping;
using FluentValidation;
using onlineCinema.Validators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
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

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
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
builder.Services.AddScoped<SessionMapper>();

builder.Services.AddValidatorsFromAssemblyContaining<BookingInputViewModelValidator>();

builder.Services.AddFluentValidationAutoValidation();
var app = builder.Build();

////////////////////////////////////////////////////////////////////
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<onlineCinema.Infrastructure.Data.ApplicationDbContext>();
        var userManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<onlineCinema.Domain.Entities.ApplicationUser>>();

        // Викликаємо наш метод
        await onlineCinema.Infrastructure.Data.DbInitializer.Initialize(context, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Сталася помилка під час заповнення БД.");
    }
}
////////////////////////////////////////////////////////////////////

// Налаштування локалізації
var supportedCultures = new[] { new CultureInfo("uk-UA") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("uk-UA"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
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
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
