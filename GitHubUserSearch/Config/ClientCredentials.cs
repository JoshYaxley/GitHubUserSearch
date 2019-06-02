using System;

namespace GitHubUserSearch.Config
{
    public struct ClientCredentials
    {
        public string Id { get; }
        public string Secret { get; }

        public ClientCredentials(string id, string secret)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Secret = secret ?? throw new ArgumentNullException(nameof(secret));
        }
    }
}