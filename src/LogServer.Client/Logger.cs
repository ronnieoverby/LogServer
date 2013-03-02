using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using LogServer.Common;

namespace LogServer.Client
{
    public class Logger<T> : Logger
    {
        public Logger()
            : base(typeof(T))
        {

        }
    }

    /// <summary>
    /// Used to create new log entries.
    /// </summary>
    public class Logger
    {
        private static readonly object LoggersLock = new object();
        private static readonly Dictionary<Type, Logger> Loggers = new Dictionary<Type, Logger>();

        public Logger(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            Type = type;
        }

        /// <summary>
        /// The type that the logger is being used in.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Creates a logger instance for use by the calling class.
        /// </summary>
        public static Logger ForThis()
        {
            lock (LoggersLock)
            {
                // check for existence
                var type = new StackTrace().GetFrame(1).GetMethod().DeclaringType;
                System.Diagnostics.Debug.Assert(type != null, "type != null");
                if (Loggers.ContainsKey(type))
                    return Loggers[type];

                var logger = new Logger(type);
                Loggers.Add(type, logger);
                return logger;
            }
        }

        public void Write([NotNull] LogEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");
            throw new NotImplementedException();
        }

        private const string FormatParameterName = "message";

        [StringFormatMethod(FormatParameterName)]
        public LogEntryBuilder Trace(string message = null, params object[] args)
        {
            return new LogEntryBuilder(this).Message(message, args).Trace().Message(message, args);
        }

        [StringFormatMethod(FormatParameterName)]
        public LogEntryBuilder Debug(string message = null, params object[] args)
        {
            return new LogEntryBuilder(this).Message(message, args).Debug();
        }

        [StringFormatMethod(FormatParameterName)]
        public LogEntryBuilder Info(string message = null, params object[] args)
        {
            return new LogEntryBuilder(this).Message(message, args).Info();
        }

        [StringFormatMethod(FormatParameterName)]
        public LogEntryBuilder Warn(string message = null, params object[] args)
        {
            return new LogEntryBuilder(this).Message(message, args).Warn();
        }

        [StringFormatMethod(FormatParameterName)]
        public LogEntryBuilder Error(string message = null, params object[] args)
        {
            return new LogEntryBuilder(this).Message(message, args).Error();
        }

        [StringFormatMethod(FormatParameterName)]
        public LogEntryBuilder Fatal(string message = null, params object[] args)
        {
            return new LogEntryBuilder(this).Message(message, args).Fatal();
        }
    }
}
