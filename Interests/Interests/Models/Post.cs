using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interests.Models;

namespace Interests.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public ApplicationUser Author { get; set; }
       // public string AuthorId => Author.Id;
        public byte[] Image { get; set; }
        public string ImageUrl => "/Posts/GetPostImage/" + Id;
        public string LinkUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }

        public static void FixLinkUrls()
        {
            var db = new ApplicationDbContext();
            foreach (var p in db.Posts)
            {
                p.LinkUrl = "http://google.com";
            }
            db.SaveChanges();
        }
    }

}