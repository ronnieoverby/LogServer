using System;
using LogServer.Common;

namespace LogServer.Core.Conditions
{
    public class SourceCondition : Condition
    {
        public string ApplicationName { get; set; }

        protected override bool Test(LogEntry entry)
        {
            var names = Tuple.Create(ApplicationName ?? "", entry.ApplicationName ?? "");
            return names.Item1.Equals(names.Item2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}