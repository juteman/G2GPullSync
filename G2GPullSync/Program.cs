using System;
using G2GPullSync.Github;
using RestSharp;

namespace G2GPullSync
{
    class Program
    {
        static void Main(string[] args)
        {
            BotInfo.GetAllInfo();
            Console.WriteLine(BotInfo.GetName());
            Console.WriteLine(BotInfo.GetToken());
            GithubClient.AddHttpAuthenticator(BotInfo.GetName(), BotInfo.GetToken());

            var request = new RestRequest("/users/juteman/repos");

            var userRepos = GithubClient.Client.Get(request);
            
            Console.WriteLine(userRepos.Content);
        }
    }
}