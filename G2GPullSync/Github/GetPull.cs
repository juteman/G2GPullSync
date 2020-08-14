using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace G2GPullSync.Github
{
    public class Pull
    {
        public List<PullRequestDesc> PullRequestInfo { get; set; } = new List<PullRequestDesc>();

        /// <summary>
        /// Get all the Pull request under the project
        /// </summary>
        /// <param name="repoName">repo name to get</param>
        /// <returns></returns>
        public async Task GetPullRequestAsync(string repoName)
        {
            int page = 1;
            bool status;
            do
            {
                var response = await GithubClient.GetResponseAsync<List<PullRequestDesc>>(SetRequest(repoName, page));

                foreach (var desc in response.Data)
                {
                    PullRequestInfo.Add(desc);
                }

                status = GithubClient.HaveNextpage(response);
                page++;
            } while (status);
        }

        private RestRequest SetRequest(string repoName, int page = 1, int pageSize = 100)
        {
            RestRequest request = new RestRequest($"repos/{BotInfo.GetOwner()}/{repoName}/pulls");
            // Get the open state pull request default
            request.AddParameter("page", page);
            request.AddParameter("per_page", pageSize);
            return request;
        }

        /// <summary>
        /// Struct for pull request infomation
        /// </summary>
        public class PullRequestDesc
        {
            public uint Number { get; set; }

            public string State { get; set; }

            public string Body { get; set; }

            public string CreatedAt { get; set; }

            public HeadDesc Head { get; set; }

            public UserDesc User { get; set; }

            public class HeadDesc
            {
                public string Ref { get; set; }
                public string Sha { get; set; }
            }

            public class UserDesc
            {
                public string Login { get; set; }
                public uint Id { get; set; }
            }
        }
    }
}