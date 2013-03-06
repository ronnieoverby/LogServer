using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LogServer.Common;
using System.Linq;

namespace LogServer.Core.Conditions
{
    public class HasDataKeyCondition : Condition
    {
        public List<string> DataKeys { get; set; }
        public bool MustContainAll { get; set; }

        protected override bool Test([NotNull] LogEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");

            // no data keys specified, test should pass
            if (DataKeys == null || DataKeys.Count == 0)
                return true;

            // datakeys specified, but no data in entry
            if (entry.Data == null || entry.Data.Count == 0)
                return false;

            Func<string, bool> predicate = dk => entry.Data.ContainsKey(dk);
            return MustContainAll ? DataKeys.All(predicate) : DataKeys.Any(predicate);
        }
    }
}