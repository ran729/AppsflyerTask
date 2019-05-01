using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace AppsflyerTwitter.DAL
{
    public class TweetsRepository : ITweetRepository
    {
        private readonly DatabaseContext _dbContext;

        public TweetsRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tweet> AddAsync(Tweet tweet)
        {
            tweet.CreatedAt = DateTime.Now;
            _dbContext.Tweets.Add(tweet);
            await _dbContext.SaveChangesAsync();
            return tweet;
        }

        public List<Tweet> GetFeed(int feedSize)
        {
            return _dbContext.Tweets
                .OrderByDescending(o => o.CreatedAt)
                .Take(feedSize)
                .ToList();
        }
    }
}
