using Newtonsoft.Json;

namespace GitHubUserSearch.Dtos
{
    public class UserDto
    {
        public string Login { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("repos_url")]
        public string RepositoriesUrl { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }
    }
}
