using LogServer.Core;
using Xunit;

namespace LogServer.Tests.Core
{
    public class BrokerTests
    {
        [Fact]
        public void CanCreateBroker()
        {
            var broker = new Broker();
        }
    }
}
