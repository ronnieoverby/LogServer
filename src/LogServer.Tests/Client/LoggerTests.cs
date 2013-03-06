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
            var type = GetType();
            Assert.Equal(type.FullName, logger.Name);
        }

        [Fact]
        public void CanCreateLoggerForSpecifiedClass()
        {
            var logger = Logger.Create<long>();
            var type = typeof (Int64);
            var logger2 = Logger.Create(type);
            Assert.Equal(type.FullName, logger.Name);
            Assert.Equal(type.FullName, logger2.Name);
        }

        [Fact]
        public void LoggersAreCachedAndReusedForTheSameType()
        {
            var l1 = Logger.ForThis();

            Assert.Same(l1, Logger.ForThis());
            Assert.Same(l1, Logger.Create<LoggerTests>());
            Assert.Same(l1, Logger.Create(GetType()));
            Assert.Same(l1, Logger.Create("LogServer.Tests.Client.LoggerTests"));
        }

        [Fact]
        public void CanLogVariousLevels()
        {
            var logger = Logger.ForThis();

            logger.Trace("A very minute thing happened.{0}", false);
            logger.Debug("Perhaps this will help you during development, {0}", 12345);
            logger.Info("Something informative for {0}.", "Ronnie");
            logger.Warn("Uh oh... This could be a problem. See: {0}", DateTime.UtcNow);
            logger.Error("Yikes, this is bad!");
            logger.Fatal("** SYSTEM DOWN | SYSTM DWNN | STm d... ___  **");
        }

        [Fact]
        public void CanConfigLogger()
        {
            Logger.Config("log1", "log2", "url=http://localhost:1337;username=ronnie;password=$tr0ngP@ss");
        }

        [Fact]
        public void CantUseGarbageConnectionStringsToConfigLogger()
        {
            Assert.Throws<FormatException>(() => Logger.Config("murder she wrote"));
        }
    }
}