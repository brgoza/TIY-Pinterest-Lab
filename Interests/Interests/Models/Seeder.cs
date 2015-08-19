using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Routing;
using System.Xml.Serialization.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interests.Models
{
    public class Seeder
    {
        public static string SeedPath = HttpContext.Current.Server.MapPath("~/App_Data/");
        public static int SeedUsers(ApplicationDbContext context)
        {
           

            var counter = 0;
            var seedUsers = new List<ApplicationUser>();
            var mgr = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var reader = File.OpenText(SeedPath  + "namesAndEmails.csv");
            while (!reader.EndOfStream)
            {
                var ln = reader.ReadLine().Split('|');
                seedUsers.Add(new ApplicationUser(ln[0], ln[1], ln[2]));
            }
            foreach (
                var usr in seedUsers.Where(usr => context.Users.FirstOrDefault(y => y.UserName == usr.UserName) == null)
                )
            {
                mgr.Create(usr, "Password?123");
                counter++;
            }
            context.SaveChanges();
            return counter;
        }

        public static int SeedPosts(ApplicationDbContext context, int countPerUser = 3)
        {
          var counter = 0;
            var imageUrls = new List<string>();
            imageUrls.AddRange(File.ReadLines(SeedPath + "postimageurls.csv"));
            var lipsums = new List<string>();
            lipsums.AddRange(File.ReadLines(SeedPath + "lipsums.csv"));
            var users = context.Users.ToList();
            foreach (var usr in users)
            {
                while (usr.MyPosts.Count < countPerUser)
                {
                    var newPost = new Post(lipsums.OrderBy(x => Guid.NewGuid()).FirstOrDefault(),
                        imageUrls.OrderBy(x => Guid.NewGuid()).FirstOrDefault(), "http://google.com");
                    usr.MyPosts.Add(newPost);
                    context.Posts.Add(newPost);
                    counter++;
                }
                context.SaveChanges();
            }
            return counter;
        }

        public static List<byte[]> GetSeedImages()
        {
            var filename = "~/App_Data/postimageurls.csv";
            var imageUrls = File.ReadLines(filename);
            return imageUrls.Select(url => Post.GetImageBytes(url)).ToList();
        }

        public static void tmp(ApplicationDbContext context)
        {
            var seedImages = GetSeedImages();
            foreach (var p in context.Posts)
            {
                p.Image = seedImages.OrderBy(x => Guid.NewGuid()).First();
            }
            context.SaveChanges();
        }

        public static void Seed(ApplicationDbContext context)
        {
         
            SeedPosts(context);
        }
    }
}