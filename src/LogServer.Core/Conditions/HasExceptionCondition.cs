using System;
using JetBrains.Annotations;
using LogServer.Common;

namespace LogServer.Core.Conditions
{
    public class HasExceptionCondition : Condition
    {
        protected override bool Test([NotNull] LogEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");
            return entry.Exception != null;
        }
    }
}