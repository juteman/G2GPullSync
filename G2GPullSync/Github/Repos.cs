using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace G2GPullSync.Github
{
    public class Repos
    {
        private string UserName { get; set; }
        public List<ReposInfo> Infos { get; set; } = new List<ReposInfo>();

        public Repos(string userName)
        {
            UserName = userName;
        }
        
        /// <summary>
        /// Owner type of repos
        /// </summary>
        public enum OwnerType
        {
            User = 0,
            Org = 1
        }

        /// <summary>
        /// Get All the repos Name
        /// </summary>
        /// <param name="ownerType">owner of repos type</param>
        /// <returns></returns>
        public async Task GetAllReposAsync(OwnerType ownerType)
        {
            int page = 1;
            do
            {
                var response = await GetResponseAsync<List<ReposInfo>>(SetRequest(ownerType, page));
                foreach (var value in response.Data)
                {
                    Infos.Add(value);
                }

                if (!GithubClient.HaveNextpage(response))
                {
                    break;
                }

                page++;
            } while (true);

            await Task.Delay(0);
        }

        /// <summary>
        /// Set the request Parameter and return the request
        /// </summary>
        /// <param name="ownerType">The type of owner</param>
        /// <param name="page">which page for repos</param>
        /// <param name="perPage">the number of repos per page have</param>
        /// <returns> the request after create </returns>
        /// <exception cref="ApplicationException">if not user or org. raise a exception</exception>
        private RestRequest SetRequest(OwnerType ownerType, int page, int perPage = 100)
        {
            RestRequest request;
            switch (ownerType)
            {
                case OwnerType.Org:
                    request = new RestRequest($"orgs/{UserName}/repos");
                    break;

                case OwnerType.User:
                    request = new RestRequest($"users/{UserName}/repos");
                    break;
                default:
                    throw new ApplicationException("Owner type no found, can't get repos");
            }

            request.AddParameter("per_page", perPage, ParameterType.UrlSegment);
            request.AddParameter("page", page, ParameterType.UrlSegment);

            return request;
        }

        /// <summary>
        /// Get Method response.
        /// </summary>
        /// <param name="request">request which is set</param>
        /// <typeparam name="T">type to serialization the data</typeparam>
        /// <returns>the response class</returns>
        /// <exception cref="ApplicationException">raise exception if can't retrieving response 20 times</exception>
        private async Task<IRestResponse<T>> GetResponseAsync<T>(RestRequest request)
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

        public class ReposInfo
        {
            public string Name { get; set; }
        }
    }
}