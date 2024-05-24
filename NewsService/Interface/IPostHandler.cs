using Microsoft.AspNetCore.Mvc;
using NewsService.Models;

namespace NewsService.Interface
{
    public interface IPostHandler
    {

        public Task<string> UpdatePost([FromForm] PostUpdateCollectioncs? value);

        public Task<string> CreatePost([FromForm] PostCollection value);
    }
}
