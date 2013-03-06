using LogServer.Core;
using LogServer.Core.Loggers;
using Xunit;

namespace LogServer.Tests.Core
{
    public class LogServerConfigTests
    {
        [Fact]
        public void CanCreateConfig()
        {
            var config = new Config();

            config.Loggers.Add(new NullLogger());
        }
    }
}