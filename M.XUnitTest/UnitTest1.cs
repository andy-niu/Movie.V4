using System;
using Xunit;

namespace M.XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var ok = true;
            Assert.True(ok);
        }
    }
}
