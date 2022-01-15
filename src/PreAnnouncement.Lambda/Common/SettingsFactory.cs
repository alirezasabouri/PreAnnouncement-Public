using Microsoft.Extensions.Configuration;

namespace PreAnnouncement.Lambda.Common
{
    internal class SettingsFactory
    {
        public static Settings CreateSettings()
        {
            var settings = new Settings();
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            config.Bind(settings);

            return settings;
        }
    }
}