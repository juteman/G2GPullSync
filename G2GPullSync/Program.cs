using System.Threading.Tasks;
using G2GPullSync.Github;

namespace G2GPullSync
{
    class Program
    {
        static async Task Main()
        {
            BotInfo.GetAllInfo();
            GithubClient.AddHttpAuthenticator(BotInfo.GetName(), BotInfo.GetToken());
            Pull pullnetty = new Pull();
            await pullnetty.GetPullRequestAsync("netty");
            PullFile getPullFile = new PullFile();
            await getPullFile.GetPullFilesAsync("netty", pullnetty.PullRequestInfo[0].Number);
            
        }
    }
}