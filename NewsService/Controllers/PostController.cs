using Microsoft.AspNetCore.Mvc;
using NewsService.Context;
using NewsService.Interface;
using NewsService.Models;
using System.Net;
using System.Reflection.Metadata;

namespace NewsService.Controllers
{ 
     [Route("api/[controller]/")]
     [ApiController]
       public class PostController : Controller
        {
            public readonly newsDbContext _context;
            public readonly IDocumentHandler _documentHandler;

        public readonly IPostHandler _postHandler;
        public PostController(newsDbContext context, IDocumentHandler documentHandler,IPostHandler postHandler)
        {

            _context = context;
            _documentHandler = documentHandler;
            _postHandler = postHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PostCollection value)
        {

            string response = await _postHandler.CreatePost(value);
            if(response == "True") 
            {
                return Json(new { success = true, message = "Record  Created Successfully" });
            }
            else
            {
                return Json(new { success = false, message = response });
            }
            
        }


       [HttpPut]
        public async Task<IActionResult> Put([FromForm] PostUpdateCollectioncs value)
            {

                string response = await _postHandler.UpdatePost(value);
                if (response == "True")
                {
                    return Json(new { success = true, message = "Record  Updated  Successfully" });
                }
                else
                {
                    return Json(new { success = false, message = response });
                }

           }

       }
}
