    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Localization;
    using System.Globalization;
    using FluentValidation;
    using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
    using onlineCinema.Validators;
    using onlineCinema.Mapping;
    using FluentValidation.AspNetCore;
    using onlineCinema.Infrastructure.Repositories;
    using onlineCinema.Application.Interfaces;
    using onlineCinema.Application.Services.Interfaces;
    using onlineCinema.Application.Services;
    using onlineCinema.Application.Mapping;
    using onlineCinema.Infrastructure.Data;
    using onlineCinema.Domain.Entities;
    using onlineCinema.Application.Configurations;

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

    builder.Services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>(options =>
    {
       
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;

       
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;

      
        options.User.RequireUniqueEmail = true;
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

       
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

    builder.Services.AddControllersWithViews(options =>
    {
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    });

    builder.Services.AddRazorPages();

    builder.Services.AddFluentValidationAutoValidation(options =>
    {
        options.DisableDataAnnotationsValidation = true;
    });
    builder.Services.AddFluentValidationClientsideAdapters();

    builder.Services.AddSingleton<MovieMapper>();
    builder.Services.AddSingleton<SessionMapper>();
    builder.Services.AddSingleton<PaymentMapper>();
    builder.Services.AddSingleton<BookingMapper>();
    builder.Services.AddSingleton<SnackMapper>();
    builder.Services.AddSingleton<HallMapper>();
    builder.Services.AddSingleton<SeatMapper>();
    builder.Services.AddSingleton<StatisticsMapping>();
    builder.Services.AddSingleton<AdminDirectorMapper>();
    builder.Services.AddScoped<CastMemberMapping>();
    builder.Services.AddScoped<FeatureMapping>();
    builder.Services.AddScoped<GenreMapping>();
    builder.Services.AddScoped<LanguageMapping>();
    builder.Services.AddScoped<DirectorMapping>();
    builder.Services.AddScoped<MovieMapping>();
    builder.Services.AddScoped<MovieScheduleViewModelMapper>();
    builder.Services.AddScoped<BookingViewModelMapper>();
    builder.Services.AddScoped<SnackViewModelMapper>();
    builder.Services.AddScoped<HallViewModelMapper>();
    builder.Services.AddScoped<SessionViewModelMapper>();
    builder.Services.AddScoped<AdminMovieMapper>();
    builder.Services.AddScoped<UserMapping>();
    builder.Services.AddScoped<AdminGenreMapper>();
    builder.Services.AddScoped<AdminCastMemberMapper>();
    builder.Services.AddScoped<AdminFeatureMapper>();
    builder.Services.AddScoped<AdminLanguageMapper>();
    builder.Services.AddScoped<AdminSnackMapper>();
    builder.Services.AddScoped<AdminStatisticsMapper>();
    builder.Services.AddScoped<AdminPaymentMapping>();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IMovieService, MovieService>();
    builder.Services.AddScoped<IDirectorService, DirectorService>();
    builder.Services.AddScoped<IBookingService, BookingService>();
    builder.Services.AddScoped<ISnackService, SnackService>();
    builder.Services.AddScoped<IHallService, HallService>();
    builder.Services.AddScoped<ISessionService, SessionService>();
    builder.Services.AddScoped<IGenreService, GenreService>();
    builder.Services.AddScoped<ICastMemberService, CastMemberService>();
    builder.Services.AddScoped<IFeatureService, FeatureService>();
    builder.Services.AddScoped<ILanguageService, LanguageService>();
    builder.Services.AddScoped<IStatisticsService, StatisticsService>();
    builder.Services.AddScoped<ITicketService, TicketService>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();

    builder.Services.AddValidatorsFromAssemblyContaining<RegisterViewModelValidator>();
    builder.Services.Configure<StatisticsSettings>(builder.Configuration.GetSection("StatisticsSettings"));

    var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<onlineCinema.Infrastructure.Data.ApplicationDbContext>();
//        var userManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<onlineCinema.Domain.Entities.ApplicationUser>>();


//        var roleManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();


//        await onlineCinema.Infrastructure.Data.DbInitializer.Initialize(context, userManager, roleManager);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "Сталася помилка під час заповнення БД.");
//    }
//}

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