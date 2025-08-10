using Duende.IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CAM.Api.Configurations.Auth
{
    public static class AuthConfigs
    {
        public static void AddAuthConfigs(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration.GetRequiredSection(nameof(AuthOptions));
            var authOptions = config.Get<AuthOptions>();

            builder.Services
                .AddAuthentication(opt =>
                {
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer("Bearer",opt =>
                {
                    opt.Authority = authOptions.Authority;
                    opt.Audience = authOptions.ClientId;
                    opt.RequireHttpsMetadata = false;
                    opt.MapInboundClaims = true;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidTypes = new[] { "at+jwt" },
                        NameClaimType = ClaimTypes.Name,
                        RoleClaimType = ClaimTypes.Role
                    };
                });

            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminOnly", policy =>
                    policy.RequireAssertion(context
                        => context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin")));
            });
        }


        public static void UseAuthConfigs(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
