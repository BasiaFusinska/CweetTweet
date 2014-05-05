using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TwitterModel;

namespace WindowsPhoneTestApp
{
    [TestClass]
    public class TwitterApiUnitTest
    {
        [TestMethod]
        public void GetTweetsTest_WPUnitTest()
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
