using System;
using JetBrains.Annotations;
using LogServer.Common;

using Lvl = LogServer.Common.Level;

namespace LogServer.Client
{
    public class LogEntryBuilder
    {
        private readonly Logger _logger;
        public LogEntry Entry { get; internal set; }

        public LogEntryBuilder([NotNull] Logger logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");
            _logger = logger;
            
            Entry = new LogEntry();
        }

        public void Write(Logger logger = null)
        {
            (logger ?? _logger).Write(Entry);
        }

        public LogEntryBuilder AddData(object obj)
        {
            throw new NotImplementedException();
            return this;
        }

        public LogEntryBuilder Exception(Exception exception)
        {
            Entry.Exception = exception;
            return this;
        }

        [StringFormatMethod("message")]
        public LogEntryBuilder Message(string message, params object[] args)
        {
            Entry.MessageFormat = message;
            Entry.MessageArgs = args;
            return this;
        }

        public LogEntryBuilder Level(Level level)
        {
            Entry.Level = level;
            return this;
        }

        public LogEntryBuilder Trace()
        {
            return Level(Lvl.Trace);
        }

        public LogEntryBuilder Debug()
        {
            return Level(Lvl.Debug);
        }

        public LogEntryBuilder Info()
        {
            return Level(Lvl.Info);
        }

        public LogEntryBuilder Warn()
        {
            return Level(Lvl.Warn);
        }

        public LogEntryBuilder Error()
        {
            return Level(Lvl.Error);
        }

        public LogEntryBuilder Fatal()
        {
            return Level(Lvl.Fatal);
        }
    }
}
