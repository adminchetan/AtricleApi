using Microsoft.AspNetCore.Mvc;
using NewsService.Context;
using NewsService.Interface;
using NewsService.Models;
using System.Net;
using System.Text;
using System.Xml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

     public class AdminController : Controller
    
     {
        public readonly newsDbContext _context;
        public readonly IDocumentHandler _documentHandler;
        public AdminController(newsDbContext context,IDocumentHandler documentHandler)
        
        {
            _context= context;
            _documentHandler = documentHandler;
        }
  
        [HttpGet]
        public IEnumerable<tbl_PostMasterMain> Get()
        {
            return _context.tbl_PostMastersMain.ToList();
       
        }


        [HttpGet("{id}")]
        public tbl_PostMasterMain Get(int id)
        {
            return _context.tbl_PostMastersMain.Where(x => x.Id ==id).FirstOrDefault();
        }


        [HttpGet("GetPostBySubCategoryId/{id}")]
        public List<tbl_PostMasterMain> GetPostBySubCategoryId(int id)
        {
            return _context.tbl_PostMastersMain.Where(x => x.SubCategoryId == id).OrderByDescending(x => x.Id).ToList();
        }

       
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] tbl_PostMasterMain value)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
