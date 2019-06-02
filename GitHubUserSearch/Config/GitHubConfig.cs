using System;
using CSharpFunctionalExtensions;

namespace GitHubUserSearch.Config
{
    public struct GitHubConfig
    {
        public string BaseUrl { get; }
        public Maybe<ClientCredentials> ClientCredentials { get; }

        public string ClientCredentialsQueryString =>
            ClientCredentials.HasValue
                ? $"client_id={ClientCredentials.Value.Id}&client_secret={ClientCredentials.Value.Secret}"
                : "";

        public GitHubConfig(string baseUrl, Maybe<ClientCredentials> clientCredentials)
        {
            BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            ClientCredentials = clientCredentials;
        }
    }
}