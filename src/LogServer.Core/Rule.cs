using System.Collections.Generic;
using LogServer.Core.Conditions;
using LogServer.Core.Loggers;

namespace LogServer.Core
{
    /// <summary>
    /// Speficifies loggers that can be written to and the conditions
    /// that must be met in order to write to them.
    /// </summary>
    public class Rule
    {
        public List<Logger> Loggers { get; set; }
        public List<Condition> Conditions { get; set; }
    }
}