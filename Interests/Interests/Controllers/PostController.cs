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
            //Configuration.tmp(db);
            return View();
        }

        public ActionResult AllInterests()
        {
            var posts = db.Posts;
            return Json(posts,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPostImage(Guid id)
        {
            var post = db.Posts.Find(id);
            return File(post.Image, "image");
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