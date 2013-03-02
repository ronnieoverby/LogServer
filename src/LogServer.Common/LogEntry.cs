using System;
using System.Collections.Generic;

namespace LogServer.Common
{
    public class LogEntry
    {
        public string ApplicationName { get; set; }
        public string TypeName { get; set; }

        public string MessageFormat { get; set; }
        public object[] MessageArgs { get; set; }

        public Level Level { get; set; }

        public Dictionary<string, object> Data { get; set; }
        public Exception Exception { get; set; }
    }
}