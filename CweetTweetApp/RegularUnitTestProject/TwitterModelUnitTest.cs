using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterModel;

namespace RegularUnitTestProject
{
    [TestClass]
    public class TwitterModelUnitTest
    {
        [TestMethod]
        public void TweetConstructorTest()
        {
            var tweet = new Tweet();
            Assert.IsNotNull(tweet);
        }
    }
}
