using System.ComponentModel.DataAnnotations;

namespace NewsService.Models
{
    public class PostCollection
    {
        public IFormFile FormFile { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SubCategoryID { get; set; }

        [Required]
        public bool IsBreaking { get; set; }

        [Required]
        public bool IsTrending { get; set; }

        [Required]
        public bool IsSaved { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string PostedBy { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Device { get; set; } = null;

        [Required]
        public bool IsDisabled { get; set; } = false;

    }
}
