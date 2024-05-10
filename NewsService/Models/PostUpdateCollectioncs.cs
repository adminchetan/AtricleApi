using System.ComponentModel.DataAnnotations;

namespace NewsService.Models
{
    public class PostUpdateCollectioncs
    {
        public int postId { get; set; }
        public IFormFile? FormFile { get; set; } // if user  changed featured image

        public string? featuredImage { get; set; } // if user not changed featured image
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryID { get; set; }
        public bool IsBreaking { get; set; }
        public bool IsTrending { get; set; }
        public bool IsSaved { get; set; }
        public string Body { get; set; }
        public string PostedBy { get; set; }
        public DateTime DateTime { get; set; }
        public string Device { get; set; } = null;
        public bool IsDisabled { get; set; } = false;
    }
}
