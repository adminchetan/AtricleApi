namespace NewsService.DTO
{
    public class PostDetailDTO
    {
        public int Id { get; set; }

        public string FeaturedMedia { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsBreaking { get; set; }
        public bool IsTrending { get; set; }
        public bool IsSaved { get; set; }
        public string Body { get; set; }
        public string PostedBy { get; set; }
        public DateTime DateTime { get; set; }
        public string Device { get; set; } = null;
        public bool IsDisabled { get; set; } = false;
        public int HitsCounts { get; set; }
        public int LikesCounts { get; set; }
        public int DislikeCounts { get; set; }
    }
}
