    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.Google;
    using Microsoft.EntityFrameworkCore;
    using Mywebapi.Auth.Services;
    using Mywebapi.Models;
using Mywebapi.Services;



    var builder = WebApplication.CreateBuilder(args);

//Add dbcontext
builder.Services.AddDbContext<AppDbContext>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        options.EnableSensitiveDataLogging();
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    );

    // Add services to the container.
    builder.Services.AddControllers();

builder.Services.AddScoped<PersonalPlanService>();

builder.Services.AddScoped<PersonalPlanApprovalService>();

builder.Services.AddScoped<PersonalPlanTaskService>();



builder.Services.AddScoped<UnitAttachmentService>();


builder.Services.AddScoped<UnitPlanApprovalService>();


builder.Services.AddScoped<UnitPlanTaskService>();


builder.Services.AddScoped<UnitPlanService>();


builder.Services.AddScoped<ManagerEvaluationService>();


builder.Services.AddScoped<SelfEvaluationService>();


builder.Services.AddScoped<EvaluationCriterionService>();

builder.Services.AddScoped<DepartmentService>();

builder.Services.AddScoped<UserService>();


//add 
builder.Services.AddHttpClient<AuthService>();

    // Add auth gg
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    }).AddCookie()
    .AddGoogle(o =>
    {
        o.ClientId = "285050056490-hs3dp3snegcbb6qpusom13hdavednm86.apps.googleusercontent.com";
        o.ClientSecret = "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";
        o.CallbackPath = "/signin-google";
    });

    // Add session
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(o =>
    {
        o.IdleTimeout = TimeSpan.FromMinutes(3);
        o.Cookie.HttpOnly = true;
        o.Cookie.IsEssential = true;
    });
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorage"));


var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
    app.UseSwagger();
    app.UseSwaggerUI();
}

    app.UseSession();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.UseStaticFiles();

    app.Run();
