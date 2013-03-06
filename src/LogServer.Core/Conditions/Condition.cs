using System;
using JetBrains.Annotations;
using LogServer.Common;

namespace LogServer.Core.Conditions
{
    public abstract class Condition
    {
        public bool Invert { get; set; }
        
        internal bool TestInternal([NotNull] LogEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");

            var test = Test(entry);
            
            if (Invert) test = !test;
            
            return test;
        }

        protected abstract bool Test(LogEntry entry);
    }
}