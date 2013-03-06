using System;
using System.Collections.Generic;
using LogServer.Common;
using LogServer.Core.Conditions;
using Xunit;

namespace LogServer.Tests.Core
{
    public class ConditionTests
    {
        [Fact]
        public void CanInvertConditionTestResult()
        {
            var e = new LogEntry();
            var c = new HasExceptionCondition();
            
            Assert.False(c.TestInternal(e));
            c.Invert = true;
            Assert.True(c.TestInternal(e));
        }

        [Fact]
        public void CanTestLogEntryForException()
        {
            var e = new LogEntry();
            var c = new HasExceptionCondition();
            
            Assert.False(c.TestInternal(e));
            
            e.Exception = new Exception();
            Assert.True(c.TestInternal(e));
        }

        [Fact]
        public void CanTestLogEntryForDataKey()
        {
            var e = new LogEntry();
            var c = new HasDataKeyCondition();
            Assert.True(c.TestInternal(e));

            c.DataKeys = new List<string> {"alpha", "beta"};
            Assert.False(c.TestInternal(e));

            e.Data["alpha"] = 'z';
            Assert.True(c.TestInternal(e));

            c.MustContainAll = true;
            Assert.False(c.TestInternal(e));

            e.Data["beta"] = 123.45m;
            Assert.True(c.TestInternal(e));
        }

        [Fact]
        public void CanTestEntryForAppName()
        {
            var e = new LogEntry();
            var c = new SourceCondition();
            Assert.True(c.TestInternal(e));

            e.ApplicationName = "my happy app";
            Assert.False(c.TestInternal(e));

            c.ApplicationName = "some other app";
            Assert.False(c.TestInternal(e));

            c.ApplicationName = e.ApplicationName;
            Assert.True(c.TestInternal(e));
        }

        [Fact]
        public void CanTestLogEntryForLevels()
        {
            var e = new LogEntry();
            Assert.Equal(Level.Trace, e.Level);

            var c = new HasLevelsCondition();
            Assert.True(c.TestInternal(e));

            c.Levels.Add(Level.Warn);
            c.Levels.Add(Level.Error);
            c.Levels.Add(Level.Fatal);
            Assert.False(c.TestInternal(e));

            e.Level = Level.Error;
            Assert.True(c.TestInternal(e));

            e.Level = Level.Info;
            Assert.False(c.TestInternal(e));

            c.Invert = true;
            Assert.True(c.TestInternal(e));

        }
    }
}
