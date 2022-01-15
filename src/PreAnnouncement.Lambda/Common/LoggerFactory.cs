using Amazon;
using Amazon.CloudWatchLogs;
using PreAnnouncement.Lambda.Common;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.AwsCloudWatch;
using System;

namespace PreAnnouncement.Lambda.Common
{
    internal class LoggerFactory
    {
        public static ILogger CreateLogger(LoggerSettings settings)
        {
            var logLevel = typeof(LogEventLevel).IsEnumDefined(settings.MinimumLogLevel) ?
                                Enum.Parse<LogEventLevel>(settings.MinimumLogLevel) : LogEventLevel.Error;
            var retentionPolicy = typeof(LogGroupRetentionPolicy).IsEnumDefined(settings.RetentionPolicy) ?
                              Enum.Parse<LogGroupRetentionPolicy>(settings.RetentionPolicy): LogGroupRetentionPolicy.OneWeek;
            var region = RegionEndpoint.GetBySystemName(settings.Region);
            var levelSwitch = new LoggingLevelSwitch();
            levelSwitch.MinimumLevel = logLevel;
 
            var options = new CloudWatchSinkOptions
            {
                LogGroupName = settings.LogGroup,
                MinimumLogEventLevel = logLevel,
                BatchSizeLimit = 100,
                QueueSizeLimit = 10000,
                Period = TimeSpan.FromSeconds(10),
                CreateLogGroup = true,
                LogStreamNameProvider = new DefaultLogStreamProvider(),
                RetryAttempts = 5,
                LogGroupRetentionPolicy = retentionPolicy
            };

            var client = new AmazonCloudWatchLogsClient(region);
            
            return new LoggerConfiguration()
                .WriteTo.Logger(l1 => l1
                    .MinimumLevel.ControlledBy(levelSwitch)
                    .WriteTo.AmazonCloudWatch(options, client))
              .CreateLogger();
        }
    }
}
