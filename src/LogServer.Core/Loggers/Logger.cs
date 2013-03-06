using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LogServer.Common;

namespace LogServer.Core.Loggers
{
    public abstract class Logger
    {
        public virtual void Write([NotNull] IEnumerable<LogEntry> entries)
        {
            if (entries == null) throw new ArgumentNullException("entries");

            foreach (var entry in entries)
                Write(entry);
        }

        protected virtual void Write([NotNull] LogEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");
            Write(string.Format(entry.MessageFormat, entry.MessageArgs));
        }

        protected virtual void Write(string entryMessage) { }
    }
}