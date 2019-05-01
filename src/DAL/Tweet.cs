using System;

namespace AppsflyerTwitter.DAL
{
    public class Tweet 
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserName { get; set; }
        public string MessageText { get; set; }
    }
}
