using System;
using LogServer.Common;
using Xunit;
using System.Linq;

namespace LogServer.Tests.Common
{
    public class LevelTests
    {
        [Fact]
        public void VerifyLevelOrder()
        {
            // state which levels are less than other levels
            var pairs = new[]
                {
                    Tuple.Create(Level.Trace, Level.Debug),
                    Tuple.Create(Level.Debug, Level.Info),
                    Tuple.Create(Level.Info, Level.Warn),
                    Tuple.Create(Level.Warn, Level.Error),
                    Tuple.Create(Level.Error, Level.Fatal)
                };

            // ensure statements above are correct
            Assert.True(pairs.All(x => x.Item1 < x.Item2));

            // ensure all levels are being tested
            Assert.Equal(Enum.GetValues(typeof (Level)).Length,
                         pairs.Select(x => x.Item1).Distinct().Count() + 1);
        }
    }
}