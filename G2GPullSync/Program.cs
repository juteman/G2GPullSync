using System;
using System.Threading.Tasks;
using G2GPullSync.Github;
using RestSharp;

namespace G2GPullSync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BotInfo.GetAllInfo();
            GithubClient.AddHttpAuthenticator(BotInfo.GetName(), BotInfo.GetToken());
            Pull pullnetty = new Pull();
            await pullnetty.GetPullRequestAsync("netty");
            
        }
    }
}