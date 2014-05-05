using System.Net;
using System.Threading.Tasks;

namespace TwitterApi
{
    public class RequestResponse
    {
        public static async void GetResponseHttp(string uriString)
        {
            var request = WebRequest.Create(uriString) as HttpWebRequest;
            
            //var response = request.GetResponse();
            var response = await request.GetResponseAsync();
        }
    }

    public static class RequestResponseExtensions
    {
        public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
        {
            var taskComplete = new TaskCompletionSource<HttpWebResponse>();
            request.BeginGetResponse(asyncResponse =>
            {
                try
                {
                    var responseRequest = (HttpWebRequest)asyncResponse.AsyncState;
                    var someResponse = (HttpWebResponse)responseRequest.EndGetResponse(asyncResponse);
                    taskComplete.TrySetResult(someResponse);
                }
                catch (WebException webExc)
                {
                    taskComplete.TrySetException(webExc);
                }
            }, request);
            return taskComplete.Task;
        }
    }
}
