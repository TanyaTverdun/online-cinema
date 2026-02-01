using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

using onlineCinema.Infrastructure.Data;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Services;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Repositories;
using onlineCinema.Validators;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Mapping;
using onlineCinema.Application.Mapping;
using System.ComponentModel.Design;

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


builder.Services.AddControllersWithViews(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<MovieFormValidator>();
FluentValidation.AspNetCore.FluentValidationMvcExtensions.AddFluentValidationClientsideAdapters(builder.Services);

builder.Services.AddScoped<MovieScheduleViewModelMapper>();
builder.Services.AddSingleton<MovieMapper>();
builder.Services.AddSingleton<SessionMapper>();
builder.Services.AddSingleton<PaymentMapper>();
builder.Services.AddSingleton<BookingMapper>();
builder.Services.AddSingleton<SnackMapper>();
builder.Services.AddSingleton<BookingViewModelMapper>();
builder.Services.AddSingleton<SnackViewModelMapper>();
builder.Services.AddSingleton<HallMapper>();
builder.Services.AddSingleton<HallViewModelMapper>();
builder.Services.AddSingleton<SeatMapper>();
builder.Services.AddSingleton<SessionViewModelMapper>();
builder.Services.AddScoped<DirectorMapping>();
builder.Services.AddScoped<MovieMapping>();
builder.Services.AddScoped<AdminMovieMapper>();
builder.Services.AddSingleton<AdminDirectorMapper>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ISnackService, SnackService>();
builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<ISessionService, SessionService>();

var app = builder.Build();

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
