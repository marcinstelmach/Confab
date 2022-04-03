namespace Confab.Shared.Infrastructure.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Confab.Shared.Abstractions.Auth;
    using Confab.Shared.Abstractions.Modules;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class Extensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IEnumerable<IModule> modules, Action<JwtBearerOptions> optionsFactory = null)
        {
            using var provider = services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            var settings = configuration.GetSettings<AuthSettings>();
            services.Configure<AuthSettings>(x => configuration.GetSection(nameof(AuthSettings)).Bind(x));
            services.AddTransient<IAuthManager, AuthManager>();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidAudience = settings.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = settings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey)),
                        // ClockSkew = TimeSpan.FromDays(5)
                    };
                });

            var policies = modules?.SelectMany(x => x.Policies) ?? Enumerable.Empty<string>();
            services.AddAuthorization(x =>
            {
                foreach (var policy in policies)
                {
                    x.AddPolicy(policy, y => y.RequireClaim("permissions", policy));
                }
            });

            return services;
        }
    }
}