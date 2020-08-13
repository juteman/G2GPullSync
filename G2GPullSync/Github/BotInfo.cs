using System.IO;
using Microsoft.Extensions.Configuration;

namespace G2GPullSync.Github
{
    /// <summary>
    /// Class restore the github bot name
    /// </summary>
    public class BotInfo
    {
        private static string BotName { get; set; }
        private static string Token { get; set; }
        
        private static string OwnerName { get; set; }

        /// <summary>
        /// Read the information from the json file and set the value
        /// </summary>
        public static void GetAllInfo()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Profile.json");

            var configuration = builder.Build();
            BotName = configuration.GetSection("bot_name").Value;
            Token = configuration.GetSection("token").Value;
            OwnerName = configuration.GetSection("owner_name").Value;
        }
        
        /// <summary>
        /// Get the GitHub bot name
        /// </summary>
        /// <returns>the name of bot</returns>
        public static string GetName()
        {
            return BotName;
        }
        
        /// <summary>
        /// Get the token of the bot
        /// </summary>
        /// <returns>return the token of bot </returns>
        public static string GetToken()
        {
            return Token;
        }
        
        /// <summary>
        /// Get the owner name of repos. It will be user name or org name 
        /// </summary>
        /// <returns>owner name</returns>
        public static string GetOwner()
        {
            return OwnerName;
        }
    }
}