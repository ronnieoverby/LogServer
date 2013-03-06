using System.Collections.Generic;
using LogServer.Common;
using System.Linq;

namespace LogServer.Core.Conditions
{
    public class HasLevelsCondition : Condition
    {
        private readonly List<Level> _levels = new List<Level>();
        public List<Level> Levels
        {
            get { return _levels; }
        }

        protected override bool Test(LogEntry entry)
        {
            return Levels == null || Levels.Count == 0 || Levels.Any(x => x == entry.Level);
        }
    }
}