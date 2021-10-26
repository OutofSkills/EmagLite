using DataAccess;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Newtonsoft.Json.Serialization;
using Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTApi.Services.Intefaces;
using RESTApi.Services;
using RESTApi.Services.Interfaces;
using RESTApi.Services.Helpers;

namespace RESTApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            var appSettingsSection = Configuration.GetSection("ApiSettings");
            services.Configure<ApiSettings>(appSettingsSection);

            var apiSettings = appSettingsSection.Get<ApiSettings>();
            var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
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
            services.AddCors(o => o.AddPolicy("EmagLite", builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddSwaggerGen(c =>
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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("EmagLite");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
