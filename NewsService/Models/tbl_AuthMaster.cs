using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsService.Models
{
    [Table("tbl_AuthMaster", Schema = "dbo")]
    public class tbl_AuthMaster
    {
        [Key]
        public int UserId { get; set; }

        public string MobileNumber { get; set; }//Defined Unique in DBcontextClass
        public string? FirstName {  get; set; }
        public string? LastName { get; set; } = string.Empty;
        public string Email { get; set; } //Defined Unique in DBcontextClass
        public string Hash { get; set; }    
        public string Salt { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? isActive { get; set; } = false;
    }
}
