using System.Collections.Generic;
using LogServer.Core.Loggers;

namespace LogServer.Core
{
    /// <summary>
    /// Holds the log server's current configuration including loggers and filters.
    /// </summary>
    public class Config
    {
        private readonly List<Logger> _loggers = new List<Logger>();
        public List<Logger> Loggers
        {
            get { return _loggers; }
        }

        public Config()
        {
        }
    }
}