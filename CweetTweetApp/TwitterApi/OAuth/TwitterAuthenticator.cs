using RestSharp;
using RestSharp.Authenticators;

namespace TwitterApi.OAuth
{
    public class TwitterAuthenticator
    {
        public static IAuthenticator GetTwitterAuthenticator()
        {
            return OAuth1Authenticator.ForProtectedResource(OAuthData.ConsumerKey,
                                                                      OAuthData.ConsumerSecret,
                                                                      OAuthData.AccessToken,
                                                                      OAuthData.TokenSecret);
        }

    }
}
