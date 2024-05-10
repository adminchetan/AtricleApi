using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewsService.Models
{
    [Table("tbl_SubCategoryMaster", Schema = "dbo")]
    public class tbl_SubCategoryMaster
    {
          
       
            [Key]
            public int subcategroyid { get; set; }

            [Required]
            public string subcategoryName { get; set; }

            public bool Disabled { get; set; }

            // Foreign key property
            public int CategoryId { get; set; }


        public tbl_SubCategoryMaster()
            {
            Disabled = true; // Setting default value to false
            }

            // Navigation property
            [ForeignKey("CategoryId")]
            public virtual tbl_CategoryMaster Category { get; set; }


    }
}
