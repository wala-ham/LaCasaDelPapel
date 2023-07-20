using Gss.Application.Common.Interfaces.Authentication;
using Gss.Application.Common.Interfaces.Services;
using Gss.Infrastructure.Authentication;
using Gss.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace Gss.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,ConfigurationManager configuration)
    {

        services.Configure <JwtSettings> (configuration.GetSection(JwtSettings.SectionName));

        //services.Configure<JwtSettings>(options =>
        //{
        //    configuration.Bind("JwtSettings", options);
        //});

        //var jwtSettings = new JwtSettings();
        //configuration.Bind(JwtSettings.SectionName, jwtSettings);
        //services.AddSingleton(Options.Create(jwtSettings));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider,DateTimeProvider>();
        return services;
    }

}

