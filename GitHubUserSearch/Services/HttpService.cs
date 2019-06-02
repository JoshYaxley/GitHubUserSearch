using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace GitHubUserSearch.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Get<T>(Uri uri) => await SendHttpRequestAndDeserializeResponse<T>("GET", uri, false);

        public async Task<Maybe<T>> MaybeGet<T>(Uri uri) => await SendHttpRequestAndDeserializeResponse<T>("GET", uri, true);

        private async Task<T> SendHttpRequestAndDeserializeResponse<T>(string method, Uri uri, bool allowNotFoundResponse)
        {
            using (var request = new HttpRequestMessage(GetMethod(method), uri))
            {
                var response = await _httpClient.SendAsync(request);

                if (allowNotFoundResponse && response.StatusCode == HttpStatusCode.NotFound)
                    return default;

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"{(int) response.StatusCode} {response.ReasonPhrase}");

                var responseJson = await response.Content.ReadAsStringAsync();

                return string.IsNullOrEmpty(responseJson)
                    ? default
                    : JsonConvert.DeserializeObject<T>(responseJson);
            }
        }

        private static HttpMethod GetMethod(string method)
        {
            switch (method.ToUpper())
            {
                case "GET":
                    return HttpMethod.Get;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
