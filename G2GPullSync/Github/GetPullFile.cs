using RestSharp;

namespace G2GPullSync.Github
{
    public class PullFile
    {
        private RestRequest SetRequest(string repos, uint pullNum, int page = 1, int pageSize = 100)
        {
            RestRequest request = new RestRequest($"repos/{BotInfo.GetOwner()}/{repos}/pulls/{pullNum}/files");
            request.AddParameter("page", page);
            request.AddParameter("per_page", pageSize);
            return request;
        }

        public class PullFileDesc
        {
            public string Filename { get; set; }
            public uint Deletions { get; set; }
            public uint Additions { get; set; }
            public uint Changes { get; set; }
            public string BlobUrl { get; set; }
            public string RawUrl { get; set; }
            public string ContentsUrl { get; set; }
        }
    }
}