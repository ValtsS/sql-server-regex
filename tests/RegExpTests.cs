using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace tests
{
    public class RegExpTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Match()
        {
            var data = UDF.Match("123", "1");
            Assert.IsNotNull(data);
            Assert.AreEqual("1", data.Value);
        }

        [Test]
        public void Should_GroupMatch()
        {
            var data = UDF.GroupMatch("123", "(?<heh>1)", "heh");
            Assert.IsNotNull(data);
            Assert.AreEqual("1", data.Value);
        }

        [Test]
        public void Should_Replace()
        {
            var data = UDF.Replace("123", "1", "7");
            Assert.IsNotNull(data);
            Assert.AreEqual("723", data.Value);
        }

        [Test]
        public void Should_Return_Matches()
        {
            var data = UDF.Matches("111", "1");
            Assert.IsNotNull(data);
            var enumdata = data as IEnumerable<UDF.RegexMatch>;
            Assert.IsNotNull(enumdata);
            Assert.AreEqual(3, enumdata.Count());
            foreach (var entry in enumdata)
            {
                Assert.AreEqual("1", entry.MatchText.Value);
            }
        }
        [Test]
        public void Should_Split()
        {
            var input = "How do I split the string";
            var data = UDF.Split(input, "\\s");
            Assert.IsNotNull(data);
            Assert.IsNotNull(data);
            var enumdata = data as IEnumerable<UDF.RegexMatch>;
            Assert.IsNotNull(enumdata);
            Assert.AreEqual(6, enumdata.Count());
            Assert.AreEqual(input, string.Join(" ", enumdata.Select(e => e.MatchText.Value)));
        }

    }
}