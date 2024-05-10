using Microsoft.EntityFrameworkCore;
using NewsService.Models;

namespace NewsService.Context
{
    public class newsDbContext:DbContext
    {

        public newsDbContext(DbContextOptions<newsDbContext> options):base(options)
        {            
        }

        public DbSet<tbl_PostMasterMain> tbl_PostMastersMain { get; set; }

        public DbSet<tbl_UploadedFilesForPost> tbl_UploadedFiles { get; set; }

        public DbSet<tbl_CategoryMaster> tbl_CategoryMasters { get; set; }

        public DbSet<tbl_SubCategoryMaster> tbl_SubCategoryMasters { get; set; }


        /** Error Log Tables */
        public DbSet<tbl_FileUploadErrorLog> tbl_FileUploadErrorLogs { get; set; }
        /** Error Log Tables */


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_PostMasterMain>().Property(e => e.Body).IsUnicode(true);
            modelBuilder.Entity<tbl_PostMasterMain>().Property(e => e.Title).IsUnicode(true);
            modelBuilder.Entity<tbl_PostMasterMain>().Property(e => e.PostedBy).IsUnicode(true);
            modelBuilder.Entity<tbl_PostMasterMain>().Property(e => e.PostedBy).IsUnicode(true);
            modelBuilder.Entity<tbl_UploadedFilesForPost>().Property(a=>a.fileUrl).IsUnicode(true);

            modelBuilder.Entity<tbl_SubCategoryMaster>()
              .HasOne(s => s.Category)
              .WithMany(c => c.SubCategories)
              .HasForeignKey(s => s.CategoryId);
            /// Specify that the property should store Unicode data (UTF-8)
        }

    }
}
