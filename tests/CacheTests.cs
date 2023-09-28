using dev;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace tests
{
    public class CacheTests
    {
        [Test]
        public void Should_Succeed()
        {
            var newRe = RegexCache.Get("1");
            var cachedRe = RegexCache.Get("1");
            Assert.AreEqual(newRe, cachedRe);
        }
    }
}
