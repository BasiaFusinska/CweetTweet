using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace RequestResponseLibrary
{
    public class RequestResponse
    {
        public static async void GetResponseHttp(string uriString)
        {
            var request = WebRequest.Create(uriString) as HttpWebRequest;
            var response1 = request.GetResponse();
            var response2 = await request.GetResponseAsync();
        }

        public static async void GetResponseRestSharp(string uriString)
        {
            var restClient = new RestClient(uriString);
            var request = new RestRequest();

            var response = restClient.Execute(request);
        }

    }
}
