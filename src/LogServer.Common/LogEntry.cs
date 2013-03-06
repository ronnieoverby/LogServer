using System;
using System.Collections.Generic;

namespace LogServer.Common
{
    public class LogEntry
    {
        private readonly Dictionary<string, object> _data = new Dictionary<string, object>();
        public Guid Id { get; private set; }
        public DateTimeOffset Created { get; private set; }

        public string ApplicationName { get; set; }
        public string Source { get; set; }

        public string MessageFormat { get; set; }
        public object[] MessageArgs { get; set; }

        public Level Level { get; set; }

        public Dictionary<string, object> Data
        {
            get { return _data; }
        }

        public Exception Exception { get; set; } // todo: need class to hold exception data that can be sent across network

        public LogEntry()
        {
            Id = Guid.NewGuid();
            Created = DateTimeOffset.Now;
        }
    }
}