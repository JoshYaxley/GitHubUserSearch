using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GitHubUserSearch.Config;
using GitHubUserSearch.Dtos;

namespace GitHubUserSearch.Services
{
    public class GitHubService
    {
        private readonly HttpService _httpService;
        private readonly GitHubConfig _config;

        public GitHubService(HttpService httpService, GitHubConfig config)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            _config = config;
        }

        public async Task<Maybe<UserDto>> GetUser(string userName)
        {
            var uri = new UriBuilder(new Uri(_config.BaseUrl))
            {
                Path = $"/users/{userName}",
                Query = _config.ClientCredentialsQueryString
            }.Uri;

            return await _httpService.MaybeGet<UserDto>(uri);
        }

        public async Task<ICollection<RepositoryDto>> GetRepositories(string userName)
        {
            var uri = new UriBuilder(new Uri(_config.BaseUrl))
            {
                Path = $"/users/{userName}/repos",
                Query = _config.ClientCredentialsQueryString
            }.Uri;

            return await _httpService.Get<ICollection<RepositoryDto>>(uri);
        }
    }
}
