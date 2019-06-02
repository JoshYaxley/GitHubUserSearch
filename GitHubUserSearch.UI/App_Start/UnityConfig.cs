using GitHubUserSearch.Services;
using System;
using System.Configuration;
using Unity;
using Unity.Lifetime;
using System.Net.Http;
using System.Net.Http.Headers;
using CSharpFunctionalExtensions;
using GitHubUserSearch.Config;

namespace GitHubUserSearch.UI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterFactory<HttpClient>(_ =>
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse("github-user-search"));

                return httpClient;
            }, new SingletonLifetimeManager());

            container.RegisterFactory<GitHubConfig>(_ =>
            {
                var baseUrl = ConfigurationManager.AppSettings["GitHub:BaseUrl"];
                var clientId = ConfigurationManager.AppSettings["GitHub:ClientId"];
                var clientSecret = ConfigurationManager.AppSettings["GitHub:ClientSecret"];

                if (clientId != null && clientSecret != null)
                {
                    var clientCredentials = new ClientCredentials(clientId, clientSecret);
                    return new GitHubConfig(baseUrl, clientCredentials);
                }

                return new GitHubConfig(baseUrl, Maybe<ClientCredentials>.None);
            }, new SingletonLifetimeManager());

            container.RegisterFactory<GitHubService>(c =>
            {
                var httpService = c.Resolve<HttpService>();
                var gitHubConfig = c.Resolve<GitHubConfig>();

                return new GitHubService(httpService, gitHubConfig);
            });
        }
    }
}