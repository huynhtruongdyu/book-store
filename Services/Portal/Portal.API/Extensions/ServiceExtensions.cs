using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Portal.Domain.Core.Auth;
using Portal.Infrastructure;
using Portal.Infrastructure.EF;
using Portal.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace Portal.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            //services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var allrepositoryinterfaces = Assembly.GetAssembly(typeof(IBaseRepository<>))
                ?.GetTypes().Where(t => t.Name.EndsWith("Repository")).ToList();
            var allrepositoryimplements = Assembly.GetAssembly(typeof(BaseRepository<>))
                ?.GetTypes().Where(t => t.Name.EndsWith("Repository")).ToList();

            foreach (var repositorytype in allrepositoryinterfaces.Where(t => t.IsInterface))
            {
                var implement = allrepositoryimplements.FirstOrDefault(c => c.IsClass && repositorytype.Name.Substring(1) == c.Name);
                if (implement != null) services.AddScoped(repositorytype, implement);
            }
        }

        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            var allServicesInterfaces = Assembly.GetAssembly(typeof(IService))
                ?.GetTypes().Where(t => t.Name.EndsWith("Service")).ToList();
            var allServiceImplements = Assembly.GetAssembly(typeof(Service))
                ?.GetTypes().Where(t => t.Name.EndsWith("Service")).ToList();

            foreach (var serviceType in allServicesInterfaces.Where(t => t.IsInterface))
            {
                var implement = allServiceImplements.FirstOrDefault(c => c.IsClass && serviceType.Name.Substring(1) == c.Name);
                if (implement != null) services.AddScoped(serviceType, implement);
            }
        }

        public static void ConfigureApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
        }

        /// <summary>
        ///     Rate Limiting allows us to protect our API against too many requests that
        /// can deteriorate our API’s performance.API is going to reject requests that
        /// exceed the limit. Throttling queues exceeded requests for possible later
        /// processing.The API will eventually reject the request if processing cannot
        /// occur after a certain number of attempts.
        ///     For example, we can configure our API to create a limitation of 100
        /// requests/hour per client.Or additionally, we can limit a client to the
        /// maximum 1,000 requests/day per IP and 100 requests/hour.We can
        /// even limit the number of requests for a specific resource in our API; for
        /// example, 50 requests to api/companies.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            //services.AddMemoryCache();
            //var rateLimitRules = new List<RateLimitRule>            {
            //    new RateLimitRule {
            //         Endpoint = "*",
            //         Limit= 3,
            //         Period = "5m"
            //    }
            //};
            //services.Configure<IpRateLimitOptions>(opt => {
            //    opt.GeneralRules = rateLimitRules;
            //});
            //services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            //services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            //services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            //services.AddHttpContextAccessor();
            ////NOTE: Add app.UseIpRateLimiting();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            //var builder = services.AddIdentityCore<User>(o =>
            //{
            //    o.Password.RequireDigit = true;
            //    o.Password.RequireLowercase = false;
            //    o.Password.RequireUppercase = false;
            //    o.Password.RequireNonAlphanumeric = false;
            //    o.Password.RequiredLength = 10;
            //    o.User.RequireUniqueEmail = true;
            //});
            //builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            //builder.AddEntityFrameworkStores<BookDbContext>()
            //.AddDefaultTokenProviders();

            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BookDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }
    }

    public class ConfigureSwaggerOptions
        : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(
            IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(
                ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Heroes API",
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}