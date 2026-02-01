using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Services;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;
using onlineCinema.Infrastructure.Repositories;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using onlineCinema.Validators;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Mapping;
using onlineCinema.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

// 1. Налаштування бази даних
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        b => b.MigrationsAssembly("onlineCinema.Infrastructure")
    ));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 2. Реєстрація сервісів (Services & Repositories)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Сервіси
builder.Services.AddScoped<AdminGenreMapper>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ICastMemberService, CastMemberService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<ISnackService, SnackService>();

// 3. Реєстрація Маперів (Mapping)
// Mapperly мапери
builder.Services.AddScoped<CastMemberMapping>();
builder.Services.AddScoped<FeatureMapping>();
builder.Services.AddScoped<GenreMapping>();
builder.Services.AddScoped<LanguageMapping>();
builder.Services.AddScoped<SnackMapping>();
builder.Services.AddScoped<DirectorMapping>();
builder.Services.AddScoped<MovieMapping>();

// Ручні мапери
builder.Services.AddScoped<AdminDirectorMapper>();

// 4. Валідація
// AddFluentValidationAutoValidation - перевіряє дані на сервері.
// AddFluentValidationClientsideAdapters - дозволяє jQuery Validate розуміти правила FluentValidation (для червоних рамок).
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<DirectorFormValidator>();

builder.Services.AddControllersWithViews(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();