using System.Linq;
using LogServer.Common;
using Xunit;

namespace LogServer.Tests.Core
{
    public class LogEntryTests
    {
        [Fact]
        public void NewLogEntryHasUniqueId()
        {
            const int n = 10000;
            var distincIds = Enumerable.Range(1, n).Select(x => new LogEntry().Id).Distinct().Count();

            Assert.Equal(n, distincIds);
        }
    }
}
