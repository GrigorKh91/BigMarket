using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BigMarket.GatewaySolution.Extentions
{
    internal static class WebApplicationBuilderExtentions
    {
        internal static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            var secret = builder.Configuration.GetValue<string>("AppSettings:Secret");
            var issuer = builder.Configuration.GetValue<string>("AppSettings:Issuer");
            var audience = builder.Configuration.GetValue<string>("AppSettings:Audience");
            var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });
            return builder;
        }
    }
}
