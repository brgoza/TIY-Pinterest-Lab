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
   
        public ActionResult Index()
        {

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
       
        public ActionResult NewPost(string description,  string imageUrl, string linkUrl)
       {
            var newPost = new Post(description,imageUrl,linkUrl);
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