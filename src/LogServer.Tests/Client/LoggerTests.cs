using System;
using LogServer.Client;
using Xunit;

namespace LogServer.Tests.Client
{
    public class LoggerTests
    {
        [Fact]
        public void CanCreateLoggerForThisClass()
        {
            var logger = Logger.ForThis();
            Assert.Equal(GetType(), logger.Type);
        }

        [Fact]
        public void CanCreateLoggerForSpecifiedClass()
        {
            var logger = new Logger(typeof (Int64));

            Assert.Equal(typeof (Int64), logger.Type);
        }

        [Fact]
        public void LoggersAreCachedAndReusedForTheSameType()
        {
            var logger = Logger.ForThis();
            var logger2 = Logger.ForThis();

            Assert.Same(logger, logger2);
        }

        [Fact]
        public void CanLogVariousLevels()
        {
            var logger = Logger.ForThis();
            logger.Info("Something informative for {0}.", "Ronnie");
        }
    }
}