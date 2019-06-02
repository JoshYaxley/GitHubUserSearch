using Newtonsoft.Json;

namespace GitHubUserSearch.Dtos
{
    public class RepositoryDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }
    }
}
