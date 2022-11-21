using Ecommerce.Application.Common;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Identity.Entities;
using Ecommerce.Domain.Identity.Permissions;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Sql.DataAccess;
using Ecommerce.Infrastructure.Sql.Identity;
using Ecommerce.Infrastructure.Sql.Identity.Filters;
using Ecommerce.Infrastructure.Sql.Identity.Permission;
using Ecommerce.Infrastructure.Sql.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;

namespace Ecommerce.Infrastructure.Sql.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("AppConnection")));
            services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            var k = services.BuildServiceProvider().GetService<IKeyAccessor>();
            var conSec = k["SecurityConfiguration"] != null ? JsonSerializer.Deserialize<SecurityConfiguration>(k["SecurityConfiguration"]) : new SecurityConfiguration();
            var conAdv = k["AdvancedConfiguration"] != null ? JsonSerializer.Deserialize<AdvancedConfiguration>(k["AdvancedConfiguration"]) : new AdvancedConfiguration();
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = conSec?.IsPasswordRequireDigit ?? false;
                options.Password.RequireLowercase = conSec?.IsPasswordRequireLowercase ?? false;
                options.Password.RequireUppercase = conSec?.IsPasswordRequireUppercase ?? false;
                options.Password.RequireNonAlphanumeric = conSec?.IsPasswordRequireNonAlphanumeric ?? false;
                options.Password.RequiredLength = conSec?.PasswordRequiredLength ?? 4;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(conSec?.UsertLockoutTime ?? 0);
                options.Lockout.MaxFailedAccessAttempts = conSec?.MaxFailedAccessAttempts ?? 9999;
                options.Lockout.AllowedForNewUsers = true;
                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = conAdv?.EnableEmailConfirmation ?? false;

            }).AddEntityFrameworkStores<DataContext>()
                .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IPermissionHelper, PermissionHelper>();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddTransient<IEmailManager, EmailManager>();

            services.AddTransient<IApplicationUserManager, ApplicationUserManager>();
            services.AddTransient<IApplicationRoleManager, ApplicationRoleManager>();
            services.AddTransient<IApplicationSignInManager, ApplicationSignInManager>();
            services.AddTransient<IApplicationProfileManager, ApplicationProfileManager>();

            return services;
        }

        private static IKeyAccessor _k;
        public static void UseIdentityOption(this IApplicationBuilder app, IWebHostEnvironment env, IOptions<IdentityOptions> identityOptions, IKeyAccessor k)
        {
            _k = k;
            //var K = app.ApplicationServices.GetRequiredService<IKeyAccessor>();
            var conSec = _k["SecurityConfiguration"] != null ? JsonSerializer.Deserialize<SecurityConfiguration>(_k["SecurityConfiguration"]) : new SecurityConfiguration();

            // Password settings.
            identityOptions.Value.Password.RequireDigit = conSec?.IsPasswordRequireDigit ?? false;
            identityOptions.Value.Password.RequireLowercase = conSec?.IsPasswordRequireLowercase ?? false;
            identityOptions.Value.Password.RequireUppercase = conSec?.IsPasswordRequireUppercase ?? false;
            identityOptions.Value.Password.RequireNonAlphanumeric = conSec?.IsPasswordRequireNonAlphanumeric ?? false;
            identityOptions.Value.Password.RequiredLength = conSec?.PasswordRequiredLength ?? 4;
            identityOptions.Value.Password.RequiredUniqueChars = 1;
            // Lockout settings.
            identityOptions.Value.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(conSec?.UsertLockoutTime ?? 0);
            identityOptions.Value.Lockout.MaxFailedAccessAttempts = conSec?.MaxFailedAccessAttempts ?? 9999;
            identityOptions.Value.Lockout.AllowedForNewUsers = true;
            // User settings.
            identityOptions.Value.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            identityOptions.Value.User.RequireUniqueEmail = true;


        }
    }
}
