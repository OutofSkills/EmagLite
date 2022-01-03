
using DataAccess;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Models.Helpers;
using Newtonsoft.Json.Serialization;
using RESTApi.Services;
using RESTApi.Services.Helpers;
using RESTApi.Services.Intefaces;
using RESTApi.Services.Interfaces;
using RESTApi.Services.Middleware;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Use Serilog logging
// Build configuration
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
    .AddJsonFile("appsettings.json", false)
    .Build();

var path = config.GetValue<string>("LoggingFilePath");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.File(path)
    .CreateLogger();

builder.WebHost.UseSerilog();

// Add access to generic IConfigurationRoot
builder.Services.AddSingleton<IConfigurationRoot>(config);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = false;

    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var appSettingsSection = config.GetSection("ApiSettings");
builder.Services.Configure<ApiSettings>(appSettingsSection);

var apiSettings = appSettingsSection.Get<ApiSettings>();
var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = apiSettings.ValidAudience,
        ValidIssuer = apiSettings.ValidIssuer,
        ClockSkew = TimeSpan.Zero
    };
});

//Restrict on production
builder.Services.AddCors(o => o.AddPolicy("EmagLite", builder => {
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RESTApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Bearer token is required for authentication",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                    }
                });
});

// Custom Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenBuilder, TokenBuilder>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITypesService, TypesService>();
builder.Services.AddScoped<IBrandsService, BrandsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTApi v1"));
}

app.UseHttpsRedirection();

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

app.UseCors("EmagLite");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();