using System;
using LogServer.Client;
using Xunit;

namespace LogServer.Tests.Client
{
    public class LogServerConnectionTests
    {
        [Fact]
        public void CanCreateFromConnectionString()
        {
            var cs = "url=http://localhost:12345;username=ronwell;password=$tr0ngP@ss";
            var conn = new LogServerConnection(cs);

            Assert.Equal("http://localhost:12345", conn.Url);
            Assert.Equal("ronwell", conn.Username);
            Assert.Equal("$tr0ngP@ss", conn.Password);
        }

        [Fact]
        public void CanCreateFromConnectionStringWithoutCredentials()
        {
            var cs = "url=http://logs.xyz.com";
            var conn = new LogServerConnection(cs);

            Assert.Equal("http://logs.xyz.com", conn.Url);
            Assert.Equal(null, conn.Username);
            Assert.Equal(null, conn.Password);
        }

        [Fact]
        public void CantCreateConnectionWithoutUrl()
        {
            Assert.Throws<FormatException>(() => new LogServerConnection("usrname=ronnie"));
        }

        [Fact]
        public void ConnectionStringsAreCaseInsensitive()
        {
            var conn = new LogServerConnection("userNAME=wowza;UrL=http://logserver;PaSsWoRd=jetpack");
            Assert.Equal("http://logserver", conn.Url);
            Assert.Equal("jetpack", conn.Password);
            Assert.Equal("wowza", conn.Username);
        }

        [Fact]
        public void CanCreateConnectionsFromConfigFile()
        {
            var c1 = new LogServerConnection("log1");
            var c2 = new LogServerConnection("log2");

            Assert.NotNull(c1);
            Assert.NotNull(c2);

            Assert.Equal("http://log1.xyz.com", c1.Url);
            Assert.Equal("https://log2.xyz.com", c2.Url);

            Assert.Equal("guy", c2.Username);
            Assert.Equal("dude", c2.Password);
        }
    }
}