using Medialink.RepositoryInterfaces;
using RestSharp;
using System;
using System.Net;

namespace Medialink.ApiClient
{
    public class RestSharpClient : IApiClientRepository
    {
        private readonly IRestClient _restClient;

        public RestSharpClient(string baseUrl)
        {
            _restClient = new RestClient(baseUrl);
        }

        public string Get(string route)
        {
            IRestRequest request = new RestRequest(route);
            var response = _restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            else
            {
                throw new Exception($"{(int)response.StatusCode} Error - {response.StatusCode}, Message: {response.Content}");
            }
        }
    }
}
