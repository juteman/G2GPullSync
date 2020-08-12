using System;

namespace G2GPullSync.Github
{
    public class Repos
    {
        public string Name { get; set; }

        public enum OwnerType
        {
            User = 0,
            Org = 1
        }
        
        public Repos(string ownerName)
        {
            Name = ownerName;
        }

        public void GetAllRepos(OwnerType ownerType)
        { 
        }

        public class ReposInfo
        {
            
        }
    }
}