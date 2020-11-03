﻿using Domain.Entities.Identity;
using Infrastructure.Data.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Infrastructure.IoC.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection RegisterIdentityService(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options =>
                {
                    #region Password

                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 2;
                    options.Password.RequiredLength = 6;

                    #endregion

                    #region Lockout

                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                    #endregion

                    #region User

                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";

                    #endregion
                })
                .AddEntityFrameworkStores<CactusPassDbContext>()
                .AddDefaultTokenProviders();

            services.AddHttpContextAccessor();

            services.TryAddScoped<IUserValidator<AppUser>, UserValidator<AppUser>>();
            services.TryAddScoped<IPasswordValidator<AppUser>, PasswordValidator<AppUser>>();
            services.TryAddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IRoleValidator<AppRole>, RoleValidator<AppRole>>();

            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<AppUser>>();
            services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<AppUser>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<AppUser>, UserClaimsPrincipalFactory<AppUser, AppRole>>();

            services.TryAddScoped<UserManager<AppUser>>();
            services.TryAddScoped<SignInManager<AppUser>>();

            return services;
        }
    }
}
