using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace G2GPullSync.Github
{
    public static class GithubClient
    {
        // Set the global const variable
        public const string BaseUrl = "https://api.github.com";

        // Github Client
        public static RestClient Client { get; } = new RestClient(BaseUrl);

        public static void AddHttpAuthenticator(string username, string token)
        {
            Client.Authenticator = new HttpBasicAuthenticator(username, token);
        }
        
        /// <summary>
        /// Get Method response.
        /// </summary>
        /// <param name="request">request which is set</param>
        /// <typeparam name="T">type to serialization the data</typeparam>
        /// <returns>the response class</returns>
        /// <exception cref="ApplicationException">raise exception if can't retrieving response 20 times</exception>
        public static async Task<IRestResponse<T>> GetResponseAsync<T>(RestRequest request)
        {
            int i = 0;
            var response = GithubClient.Client.Get<T>(request);
            while (response.IsSuccessful != true)
            {
                if (i >= 20)
                {
                    const string message = "Error retrieving response.  Check inner details for more info.";
                    var responseException = new ApplicationException(message, response.ErrorException);
                    throw responseException;
                }

                Thread.Sleep(3000);
                response = GithubClient.Client.Get<T>(request);
                i++;
            }
            

            await Task.Delay(0);
            return response;
        }

        public static bool HaveNextpage(IRestResponse response)
        {
            foreach (var header in response.Headers)
            {
                if (header.Value != null)
                {
                    if (header.Value.ToString().Contains("next"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}