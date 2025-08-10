using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using CAM.Api.Configurations.Auth;
namespace CAM.Api.Configurations.Swagger
{
    public static class SwaggerConfigs
    {
        public static void AddSwaggerConfigs(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration.GetRequiredSection(nameof(AuthOptions));
            var authOptions = config.Get<AuthOptions>();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{authOptions.Authority}/protocol/openid-connect/auth"),
                            TokenUrl = new Uri($"{authOptions.Authority}/protocol/openid-connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                {"tenant","this is for tenants" }
                            }
                        },
                        
                    }
                });

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "oauth2",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        ["tenant","this is for tenants"]
                    }
                };

                c.AddSecurityRequirement(securityRequirement);

                c.SwaggerDoc("admin", new OpenApiInfo { Title = "Admin API", Version = "v1" });
                c.SwaggerDoc("users", new OpenApiInfo { Title = "User API", Version = "v1" });

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var controllerNamespace = apiDesc.ActionDescriptor.RouteValues["controller"];
                    var cad = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    var ns = cad.ControllerTypeInfo.Namespace;

                    if (docName == "admin" && ns.Contains(".Admin")) return true;
                    if (docName == "users" && ns.Contains(".Users")) return true;
                    return false;
                });
            });
        }

        public static void UseSwaggerConfigs(this WebApplication app)
        {

            var config = app.Configuration.GetRequiredSection(nameof(AuthOptions));
            var authOptions = config.Get<AuthOptions>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/users/swagger.json", "User API");

                c.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin API");
                c.RoutePrefix = "swagger";

                c.OAuthClientId(authOptions.ClientId);
                c.OAuthClientSecret(authOptions.ClientSecret);
                c.OAuthScopes("tenant");
            });
        }

    }
}
