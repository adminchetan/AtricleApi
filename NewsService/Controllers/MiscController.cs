using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsService.Context;
using NewsService.DTO;
using NewsService.Models;

namespace NewsService.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class MiscController : Controller
    {
        private readonly newsDbContext _newsDbContext;
        public MiscController(newsDbContext newsDbContext)
        {
            _newsDbContext = newsDbContext;
        }

        [HttpGet("GetCategories")]

        public async Task<ActionResult<IEnumerable<tbl_CategoryMaster>>> GetCategories()
        {
            return await _newsDbContext.tbl_CategoryMasters.ToListAsync();
        }

        [HttpGet("GetSubCategories/{categoryId}")]
        public async Task<ActionResult<IEnumerable<tbl_SubCategoryMaster>>> GetSubcategoriesByCategoryId(int categoryId)
        {
            return await _newsDbContext.tbl_SubCategoryMasters.Where(s => s.CategoryId == categoryId).ToListAsync();
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CategoryDTO categoryDto)
        {
            var category = new tbl_CategoryMaster
            {
                categoryName = categoryDto.categoryName,
                Disabled = false
            };

            _newsDbContext.tbl_CategoryMasters.Add(category);
            await _newsDbContext.SaveChangesAsync();

            return Ok(category);
        }

        [HttpPost("CreateSubCategory")]
        public async Task<IActionResult> CreateSubcategory(SubCategoryDTO subcategoryDto)
        {
            var subcategory = new tbl_SubCategoryMaster
            {
                subcategoryName = subcategoryDto.subcategoryName,
                CategoryId = subcategoryDto.CategoryId,
                Disabled = false
            };

            _newsDbContext.tbl_SubCategoryMasters.Add(subcategory);
            await _newsDbContext.SaveChangesAsync();

            return Ok(subcategory);
        }

        [HttpGet("GetCategorySubCategory")]
        public async Task<IActionResult> GetCategorySubCategory()
        {
            var query = from category in _newsDbContext.tbl_CategoryMasters
                        let subcategories = _newsDbContext.tbl_SubCategoryMasters
                                              .Where(sub => sub.CategoryId == category.categoryId && !sub.Disabled)
                                              .Select(sub => new
                                              {
                                                  SubcategoryId = sub.subcategroyid,
                                                  SubcategoryName = sub.subcategoryName,
                                                  IsDisabled = sub.Disabled
                                              }).ToList()
                        where !category.Disabled && subcategories.Any()
                        select new
                        {
                            CategoryId = category.categoryId,
                            CategoryName = category.categoryName,
                            IsCategoryDisabled = category.Disabled,
                            Subcategories = subcategories
                        };

            var result = await query.ToListAsync();

            return Json(result);

        }



        [HttpGet("GetPostsByCategory/{SubCategoryId}")]
        public async Task<List<tbl_PostMasterMain>> GetPostsByCategory(int SubCategoryId)
        {
            var query = _newsDbContext.tbl_PostMastersMain
                .Where(x => x.SubCategoryId == SubCategoryId)
                .OrderBy(x => Guid.NewGuid())
                .ThenByDescending(x => x.Id)
                .Take(15);
            var result = await query.ToListAsync();
            return result;
        }

        [HttpGet("GetRightsidePostsByCategory/{SubCategoryId}")]
        public List<tbl_PostMasterMain> GetRightsidePostsByCategory(int SubCategoryId)
        {

            var query = _newsDbContext.tbl_PostMastersMain
                .Where(x => x.SubCategoryId == SubCategoryId) // Generate random ordering
                .OrderBy(x => x.Id) // Order by DateTime in descending order
                .Take(5) // Select the top 10 randomly ordered posts
                .ToList(); // Convert the results to a list
            return query;
        }


        [HttpGet("GetRecentPosts")]

        public async Task<List<tbl_PostMasterMain>> GetRecentPosts()
        {

            var query = _newsDbContext.tbl_PostMastersMain// Generate random ordering
                .OrderBy(x => x.Id) // Order by DateTime in descending order
                .Take(5) // Select the top 10 randomly ordered posts
                .ToList(); // Convert the results to a list
            return query;
        }



        [HttpGet("GetPostbyId/{Postid}")]
        public async Task<ActionResult<tbl_PostMasterMain>> GetPostbyId(int Postid)
        {

            var post = await _newsDbContext.tbl_PostMastersMain.FirstOrDefaultAsync(x => x.Id == Postid);

            if (post != null)
            {
                post.HitsCounts += 1;
                await _newsDbContext.SaveChangesAsync();
                return post;
            }
            else
            {
                return NotFound(); // Returns 404 Not Found if the post with the specified ID is not found
            }
        }




        [HttpGet("GetPostbyExclusive")]
        public async Task<List<tbl_PostMasterMain>> GetPostbyExclusive()
        {
            var query = _newsDbContext.tbl_PostMastersMain
                .Where(x => x.IsBreaking == true)
                .OrderBy(x => x.Id) // Order by DateTime in descending order
                .Take(5) // Select the top 10 randomly ordered posts
                .ToList(); // Convert the results to a list
            return query;
        }



        //Delete/Inactive Codes

        [HttpPost("DeleteCategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await _newsDbContext.tbl_CategoryMasters.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound(); // Return 404 Not Found if category with the given ID is not found
            }

            _newsDbContext.tbl_CategoryMasters.Remove(category);
            await _newsDbContext.SaveChangesAsync();

            return Json(Ok(category.categoryName + " has been deleted successfully")); // Return the deleted category if deletion is successful
        }



        [HttpPost("DeactivateCategory/{categoryId}")]
        public async Task<IActionResult> DeactivateCategory(int categoryId)
        {
            var category = await _newsDbContext.tbl_CategoryMasters.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound(category?.categoryName + "Does not exist in system"); // Return 404 Not Found if category with the given ID is not found
            }

            category.Disabled = !category.Disabled;
            _newsDbContext.tbl_CategoryMasters.Update(category);
            await _newsDbContext.SaveChangesAsync();

            return Json(Ok("Status of " + category.categoryName + " has been changed successfully"));  // Return the updated category if update is successful
        }



        [HttpPost("DeleteSubCategory/{subcategoryId}")]
        public async Task<IActionResult> DeleteSubCaregory(int subcategoryId)
        {
            var subcategory = await _newsDbContext.tbl_SubCategoryMasters.FindAsync(subcategoryId);
            if (subcategory == null)
            {
                return NotFound(subcategory?.subcategoryName + "SubCategory Not Exist"); // Return 404 Not Found if category with the given ID is not found
            }

            _newsDbContext.tbl_SubCategoryMasters.Remove(subcategory);
            await _newsDbContext.SaveChangesAsync();

            return Json(Ok(subcategory.subcategoryName + " has been deleted successfully")); // Return the deleted category if deletion is successful
        }


        [HttpPost("DeactiveSubCategory/{subcategoryId}")]
        public async Task<IActionResult> DeactiveSubCaregory(int subcategoryId)
        {
            var subcategory = await _newsDbContext.tbl_SubCategoryMasters.FindAsync(subcategoryId);
            if (subcategory == null)
            {
                return NotFound(subcategory.subcategoryName + "Does not exist in system"); // Return 404 Not Found if category with the given ID is not found
            }
            subcategory.Disabled = !subcategory.Disabled;
            _newsDbContext.tbl_SubCategoryMasters.Update(subcategory);
            await _newsDbContext.SaveChangesAsync();

            return Json(Ok("Status of " + subcategory.subcategoryName + " has been changed successfully")); // Return the updated category if update is successful
        }

        [HttpPost("GetPostdetails")]
        public async Task<List<PostDetailDTO>> getPostDetails(DateTime fromdate, DateTime todate)
        {
            var query = from postmaster in _newsDbContext.tbl_PostMastersMain
                        join category in _newsDbContext.tbl_CategoryMasters
                        on postmaster.CategoryId equals category.categoryId
                        join subcategory in _newsDbContext.tbl_SubCategoryMasters
                        on postmaster.SubCategoryId equals subcategory.subcategroyid
                        where postmaster.DateTime >= fromdate && postmaster.DateTime <= todate
                        select new PostDetailDTO
                        {
                            Id = postmaster.Id,
                            Title = postmaster.Title,
                            CategoryId = category.categoryId,
                            CategoryName = category.categoryName,
                            SubCategoryId = subcategory.subcategroyid,
                            SubCategoryName = subcategory.subcategoryName,
                            PostedBy = postmaster.PostedBy,
                            DateTime = postmaster.DateTime,
                            HitsCounts = postmaster.HitsCounts,
                            LikesCounts = postmaster.LikesCounts,
                            DislikeCounts = postmaster.DislikeCounts,
                            IsBreaking = postmaster.IsBreaking,
                            IsTrending = postmaster.IsTrending,
                            IsSaved = postmaster.IsSaved
                        };

            // Execute the query and retrieve the results
            var result = await query.ToListAsync();
            return result;
        }

    }
}
