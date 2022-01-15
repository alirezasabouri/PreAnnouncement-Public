using System;
using System.Collections.Generic;
using System.Text;

namespace PreAnnouncement.Lambda.Common
{
    public class Settings
    {
        public LoggerSettings Logger { get; set; }
    }

    public class LoggerSettings
    {
        public string MinimumLogLevel { get; set; }
        public string RetentionPolicy { get; set; }
        public string Region { get; set; }
        public string LogGroup { get; set; }
    }
}
