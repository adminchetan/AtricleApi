using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsService.Models
{
    [Table("tbl_UploadedFilesForPost", Schema = "dbo")]
    public class tbl_UploadedFilesForPost
    {
        [Key]
        [Column("FileId", TypeName = "int")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string fileUrl { get; set; }
        public int fileType { get; set; }
    }
}
