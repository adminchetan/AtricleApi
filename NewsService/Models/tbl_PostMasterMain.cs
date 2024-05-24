using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsService.Models
{
    [Table("tbl_PostMaster", Schema = "dbo")]
    public class tbl_PostMasterMain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FeaturedMedia { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

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

        public int HitsCounts { get; set; }

        public int LikesCounts { get; set; }

        public int DislikeCounts { get; set; }
    }
}
