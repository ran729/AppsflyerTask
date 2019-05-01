using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppsflyerTwitter.DAL
{
    public interface ITweetRepository
    {
        Task<Tweet> AddAsync(Tweet entity);
        List<Tweet> GetFeed(int feedSize);
    }
}
