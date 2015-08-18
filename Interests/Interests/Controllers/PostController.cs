using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Interests.Models;

//using Week7Lab.Migrations;


namespace Interests.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        // GET: Posts
        public ActionResult Index()
        {
            //   {Post.FixLinkUrls();
            //Configuration.tmp(db);
            //  Post.DeletePostsWithoutImages();
            return View();
        }

        public ActionResult AllInterests()
        {
            var posts = db.Posts.ToList().Select(x => new { x.LinkUrl, x.ImageUrl, x.CreatedOn, x.Author, x.Id, x.Description }).ToList();
            return Json(posts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPostImage(Guid id)
        {
            var post = db.Posts.Find(id);
            return File(post.Image, "image");
        }
        public ActionResult GetImageBytes(string url)
        {
            var client = new WebClient();
            var imageArray = client.DownloadData(url);

            return File(Util.ResizeImage(imageArray, 100, 100), "image");
        }

        public ActionResult NewPost(Post newPost)
        {
            newPost.CreatedOn = DateTime.Now;
            newPost.Id = Guid.NewGuid();
            db.Posts.Add(newPost);
            db.SaveChanges();

            return Json(newPost, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}