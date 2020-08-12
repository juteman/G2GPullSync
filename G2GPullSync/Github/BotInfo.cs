using System.IO;
using Microsoft.Extensions.Configuration;

namespace G2GPullSync.Github
{
    /// <summary>
    /// Class restore the github bot name
    /// </summary>
    public class BotInfo
    {
        private static string Name { get; set; }
        private static string Token { get; set; }

        /// <summary>
        /// Read the information from the json file and set the value
        /// </summary>
        public static void GetAllInfo()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Profile.json");

            var configuration = builder.Build();
            Name = configuration.GetSection("name").Value;
            Token = configuration.GetSection("token").Value;
        }
        
        /// <summary>
        /// Get the GitHub bot name
        /// </summary>
        /// <returns>the name of bot</returns>
        public static string GetName()
        {
            return Name;
        }

        public static string GetToken()
        {
            return Token;
        }
    }
}