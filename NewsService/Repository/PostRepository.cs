using Microsoft.AspNetCore.Mvc;
using NewsService.Context;
using NewsService.Interface;
using NewsService.Models;
using System.Net;
using System.Reflection.Metadata;

namespace NewsService.Repository
{
    public class PostRepository : IPostHandler
    {
        public readonly newsDbContext _context;
        public readonly IErrorLogger _logger;
        public readonly IDocumentHandler _documentHandler;
        public PostRepository(newsDbContext context ,IErrorLogger logger,IDocumentHandler documentHandler)
        {
            _context = context;
            _logger = logger;
            _documentHandler = documentHandler;

        }

        public async Task<string> CreatePost([FromForm] PostCollection value)
        {
            tbl_PostMasterMain _tbl_PostMasterMain;
            try
            {

                string decodedHtml = WebUtility.HtmlDecode(value.Body);
                FileuploadingModelBuilder FileUploadDTO = new FileuploadingModelBuilder
                {
                    Id = 0,
                    FileName = value.Title,
                    FormFile = value.FormFile,
                    Description = "Feaured Media"
                };
                var res1 = await Task.Run(() =>
                {
                    return _documentHandler.SaveImageToDirectory(FileUploadDTO, 2);
                });
                _tbl_PostMasterMain = new tbl_PostMasterMain
                {
                    FeaturedMedia = res1, Title = value.Title, CategoryId = value.CategoryId,
                    SubCategoryId = value.SubCategoryID,IsBreaking = value.IsBreaking,
                    IsTrending = value.IsTrending,IsSaved = value.IsSaved,
                    Body = decodedHtml,PostedBy = value.PostedBy,
                    DateTime = value.DateTime,Device = value.Device,
                    IsDisabled = value.IsDisabled,
                 };
                _context.tbl_PostMastersMain.Add(_tbl_PostMasterMain);
                var i=_context.SaveChanges();
                return Convert.ToString(Convert.ToBoolean(i));
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public async Task<string> UpdatePost([FromForm] PostUpdateCollectioncs value)
        {
            var FeaturedImageFinalUrl = "";  
           
            if (value.FormFile == null)//* Make the existing Image as featued image
            {
                FeaturedImageFinalUrl = value.featuredImage;  // If value is there then user no changed Featured image
            }

            else
            {
                FileuploadingModelBuilder FileUploadDTO = new FileuploadingModelBuilder   //* Make the Uploaded Image as Image as featued image
                {
                    Id = 0,
                    FileName = value.Title,
                    FormFile = value.FormFile,
                    Description = "Feaured Media"
                };

                FeaturedImageFinalUrl = await Task.Run(() =>
                {
                    return _documentHandler.SaveImageToDirectory(FileUploadDTO, 2);
                });
            }

            var post = _context.tbl_PostMastersMain.FirstOrDefault(p => p.Id == value.postId);  //*Update Post Logic
            try
            {

                post.FeaturedMedia = FeaturedImageFinalUrl;
                post.Title = value.Title;
                post.CategoryId = value.CategoryId;
                post.SubCategoryId = value.SubCategoryID;
                post.IsBreaking = value.IsBreaking;
                post.IsTrending = value.IsTrending;
                post.IsSaved = value.IsSaved;
                post.Body = WebUtility.HtmlDecode(value.Body);
                post.PostedBy = value.PostedBy;
                //post.DateTime = value.DateTime;
                post.Device = value.Device;
                post.IsDisabled = value.IsDisabled;
                var i = _context.SaveChanges();
                return Convert.ToString(Convert.ToBoolean(i)) ;
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }




    }
}
