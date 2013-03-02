using LogServer.Common;
using Xunit;

namespace LogServer.Tests.Common
{
    public class LogEntryTests
    {
        [Fact]
        public void CanCreateLogEntry()
        {
            const string msg = "Something happened!";
            const Level lvl = Level.Info;

            var entry = new LogEntry { MessageFormat = msg, Level = lvl };

            Assert.Equal(msg, entry.MessageFormat);
            Assert.Equal(lvl, entry.Level);
        }
    }
}
