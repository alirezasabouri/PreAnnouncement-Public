using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreAnnouncement.Lambda.Common
{
    public class DumbLogger : ILogger
    {
        public void Write(LogEvent logEvent)
        {
            Console.WriteLine(logEvent.RenderMessage());
        }
    }

    internal static class CommonServiceLocator
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient<Settings>((s) => SettingsFactory.CreateSettings());
            services.AddTransient<ILogger>((s) => new DumbLogger() /*LoggerFactory.CreateLogger(s.GetService<Settings>().Logger)*/);
        }
    }
}
