using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterModel;

namespace RegularUnitTestProject
{
    [TestClass]
    public class TwitterApiUnitTest
    {
        [TestMethod]
        public void GetTweetsTest_UnitTest()
        {
            var tweets = GetTweets().Result;
            Assert.AreEqual(tweets.Length, 30);
        }

        private static async Task<Tweet[]> GetTweets()
        {
            var ta = new TwitterApi.TwitterApi();
            return await ta.GetTweetsForUser("basiafusinska", 30);
        }
    }
}
