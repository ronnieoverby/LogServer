using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using JetBrains.Annotations;
using LogServer.Common;

namespace LogServer.Client
{
    /// <summary>
    /// Used to create new log entries.
    /// </summary>
    public class Logger
    {
        private static readonly object LoggersLock = new object();
        private static readonly Dictionary<string, Logger> Loggers = new Dictionary<string, Logger>();
        private static readonly List<LogServerConnection> _connections;

        public string Name { get; private set; }

        private Logger([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            Name = name;
        }

        static Logger()
        {
            _connections = new List<LogServerConnection>();
        }

        /// <summary>
        /// Creates a logger with the given name.
        /// </summary>
        public static Logger Create([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            lock (LoggersLock)
            {
                // check for existence
                if (Loggers.ContainsKey(name))
                    return Loggers[name];

                // create/cache logger
                var logger = new Logger(name);
                Loggers.Add(name, logger);
                return logger;
            }
        }

        /// <summary>
        /// Creates a logger instance named after the passed type.
        /// </summary>
        public static Logger Create(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return Create(type.FullName);
        }

        /// <summary>
        /// Creates a logger instance named after the given type parameter.
        /// </summary>
        public static Logger Create<T>()
        {
            return Create(typeof(T));
        }

        /// <summary>
        /// Creates log server connections that will be used throughout the life of the application.
        /// </summary>
        /// <param name="connectionStringsOrNames">The connection strings (or names of configured 
        /// connection strings) to use to connect to log servers.</param>
        public static void Config(params string[] connectionStringsOrNames)
        {
            foreach (var s in connectionStringsOrNames)
                _connections.Add(new LogServerConnection(s));
        }

        /// <summary>
        /// Creates a logger instance named after the calling object's type.
        /// </summary>
        public static Logger ForThis()
        {
            var type = new StackTrace().GetFrame(1).GetMethod().DeclaringType;
            return Create(type);
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
