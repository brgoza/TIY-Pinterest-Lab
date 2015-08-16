using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
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
            return View(db.Posts.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (!ModelState.IsValid) return View(post);

            post.Id = Guid.NewGuid();
            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetImage(Guid id)
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