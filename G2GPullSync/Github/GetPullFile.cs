using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace G2GPullSync.Github
{
    public class PullFile
    {
        public List<PullFileDesc> PullFileInFo { get; set; } = new List<PullFileDesc>();
        
        /// <summary>
        /// Get pull request file information
        /// </summary>
        /// <param name="repos">repos name</param>
        /// <param name="pullNum">pull request Id name</param>
        /// <returns></returns>
        public async Task GetPullFilesAsync(string repos, uint pullNum)
        {
            int page = 1;
            bool status;
            do
            {
                var response = await GithubClient.GetResponseAsync<List<PullFileDesc>>(SetRequest(repos, pullNum, page));
                
                foreach (var desc in response.Data)
                {
                    PullFileInFo.Add(desc);
                }

                status = GithubClient.HaveNextpage(response);
                page++;
            } while (status);
           
            Console.WriteLine(PullFileInFo.Count);
        }
        
        /// <summary>
        /// set the request for get pull request file
        /// </summary>
        /// <param name="repos">repos name</param>
        /// <param name="pullNum">pull request number</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private RestRequest SetRequest(string repos, uint pullNum, int page = 1, int pageSize = 100)
        {
            RestRequest request = new RestRequest($"repos/{BotInfo.GetOwner()}/{repos}/pulls/{pullNum}/files");
            request.AddParameter("page", page);
            request.AddParameter("per_page", pageSize);
            return request;
        }

        public class PullFileDesc
        {
            public string FileName { get; set; }
            public int Deletions { get; set; }
            public int Additions { get; set; }
            public int Changes { get; set; }
            public string BlobUrl { get; set; }
            public string RawUrl { get; set; }
            public string ContentsUrl { get; set; }
            public string Patch { get; set; }
        }
    }
}