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
            Console.WriteLine(BotInfo.GetName());
            Console.WriteLine(BotInfo.GetToken());
            GithubClient.AddHttpAuthenticator(BotInfo.GetName(), BotInfo.GetToken());
            Repos repos = new Repos("juteman");
            await repos.GetAllReposAsync(Repos.OwnerType.User);
            foreach (var variable in repos.Infos)
            {
                Console.WriteLine(variable.Name);
            }
        }
    }
}