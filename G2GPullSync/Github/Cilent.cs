using RestSharp;
using RestSharp.Authenticators;

namespace G2GPullSync.Github
{
    public static class  GithubClient
    {
        // Set the global const variable
        public const string BaseUrl = "https://api.github.com";

        // Github Client
        public static RestClient Client { get; } = new RestClient(BaseUrl);

        public static void AddHttpAuthenticator(string username, string token)
        {
            Client.Authenticator = new HttpBasicAuthenticator(username, token);
        }
              
    }
}