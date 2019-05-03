using System.Collections.Generic;
using System.Threading.Tasks;
using AppsflyerTwitter.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AppsflyerTwitter.Controllers
{
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private ITweetRepository _tweetRepository;

        public TweetsController(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        [HttpGet("/")]
        public ActionResult Home()
        {
            return Ok("Welcome to AppsflyerTwitter");
        }

        [HttpGet("feed")]
        public IEnumerable<Tweet> GetFeed([FromQuery]int size = 10)
        {
            return _tweetRepository.GetFeed(size);
        }

        [HttpPost("tweet")]
        public async Task<ActionResult> Post([FromBody] Tweet tweet)
        {
            var createdTweet = await _tweetRepository.AddAsync(tweet);
            return Ok(createdTweet);
        }

    }
}
