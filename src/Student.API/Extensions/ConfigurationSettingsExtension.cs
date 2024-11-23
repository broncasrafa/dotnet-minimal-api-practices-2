using Microsoft.Extensions.Options;

namespace Student.API.Extensions;

public static class ConfigurationSettingsExtension
{
    public static IServiceCollection AddConfigurationSettings<T>(this IServiceCollection services, IConfiguration configuration, string settingsName = null) where T : class
    {
        if (string.IsNullOrWhiteSpace(settingsName))
            settingsName = typeof(T).Name;

        var instance = Activator.CreateInstance<T>();
        new ConfigureFromConfigurationOptions<T>(configuration.GetSection(settingsName)).Configure(instance);
        services.AddSingleton(instance);

        return services;
    }
}
