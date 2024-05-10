using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsService.Models
{
    [Table("tbl_CategoryMaster", Schema = "dbo")]
    public class tbl_CategoryMaster
    {
        [Key]
        public int categoryId { get; set; }

        [Required]
        public string categoryName { get; set; }

        public bool Disabled { get; set; }

        public tbl_CategoryMaster()
        {
            Disabled = false; // Setting default value to false
        }

        // Navigation property
        public virtual ICollection<tbl_SubCategoryMaster> SubCategories { get; set; }
    }

    

}
