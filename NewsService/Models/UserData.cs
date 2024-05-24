using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsService.Models
{
    [Table("tbl_UserMaster", Schema = "dbo")]
    public class tbl_UserData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string? Email { get; set; }

        public int? RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? Status { get; set; }

        public string? Mobile { get; set; }


    }
}
