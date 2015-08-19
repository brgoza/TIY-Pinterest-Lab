using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Interests.Models;

namespace Interests.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public ApplicationUser Author { get; set; }
        // public string AuthorId => Author.Id;
        public string ImageUrl => "/Posts/GetPostImage/" + Id;
        public byte[] Image { get; set; }

        public string LinkUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }

        public Post()
        {}

        public Post(string description, string imageUrl, string linkUrl)
        {
           Id = Guid.NewGuid();
           CreatedOn = DateTime.Now;
           Description = description;
           Image = GetImageBytes(imageUrl);
           LinkUrl = linkUrl;
        }

        public static byte[] GetImageBytes(string url, bool resize = true)
        {
            var client = new WebClient();
            var imageArray = client.DownloadData(url);
            return ResizeImage(imageArray, 100, 100);
        }

        public static byte[] ResizeImage(byte[] source, int maxWidth, int maxHeight)
        {
            var image = System.Drawing.Image.FromStream(new MemoryStream(source));
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            Bitmap bmp = new Bitmap(newImage);

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
        
        public static void FixLinkUrls()
        {
            var db = new ApplicationDbContext();
            foreach (var p in db.Posts)
            {
                p.LinkUrl = "http://google.com";
            }
            db.SaveChanges();
        }

        public static void DeletePostsWithoutImages()
        {
            var db = new ApplicationDbContext();
            var newPosts = new List<Post>();

            foreach (var p in db.Posts.Where(p => p.Image == null))
            {
                db.Posts.Remove(p);
            }
            db.SaveChanges();
        }
    }

}