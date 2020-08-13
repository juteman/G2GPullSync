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
        public List<ReposDesc> ReposInfo { get; set; } = new List<ReposDesc>();

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
        public async Task GetReposAsync(OwnerType ownerType)
        {
            int page = 1;
            bool status = false;
            do
            {
                var response = await GithubClient.GetResponseAsync<List<ReposDesc>>(SetRequest(ownerType, page));
                foreach (var desc in response.Data)
                {
                    ReposInfo.Add(desc);
                }

                status = GithubClient.HaveNextpage(response);
                page++;
            } while (status);
        }

        /// <summary>
        /// Set the request Parameter and return the request
        /// </summary>
        /// <param name="ownerType">The type of owner</param>
        /// <param name="page">which page for repos</param>
        /// <param name="pageSize">the number of repos per page have</param>
        /// <returns> the request after create </returns>
        /// <exception cref="ApplicationException">if not user or org. raise a exception</exception>
        private RestRequest SetRequest(OwnerType ownerType, int page = 1, int pageSize = 100)
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

            request.AddParameter("page", page);
            request.AddParameter("per_page", pageSize);


            return request;
        }


        public class ReposDesc
        {
            public string Name { get; set; }
        }
    }
}