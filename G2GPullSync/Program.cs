using System;

namespace G2GPullSync
{
    class Program
    {
        static void Main(string[] args)
        {
            Github.BotInfo.GetAllInfo();
            Console.WriteLine(Github.BotInfo.GetName());
            Console.WriteLine(Github.BotInfo.GetToken());
        }
    }
}