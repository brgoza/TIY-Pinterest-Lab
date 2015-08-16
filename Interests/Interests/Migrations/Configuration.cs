using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using Interests;
using Interests.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interests.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public static int SeedUsers(ApplicationDbContext context)
        {
            const string seedPath = @"C:\Users\brgoz\OneDrive\Projects\Week7Lab\Week7Lab\Seed\";

            var counter = 0;
            var seedUsers = new List<ApplicationUser>();
            var mgr = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var reader = File.OpenText(seedPath + "namesAndEmails.csv");
            while (!reader.EndOfStream)
            {
                var ln = reader.ReadLine().Split('|');
                seedUsers.Add(new ApplicationUser(ln[0], ln[1],
                    Util.GetImageBytes("http://media.cirrusmedia.com.au/LW_Media_Library/LW-603-p28-partner-profile.jpg"),
                    ln[2]));
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
            string SeedPath = @"C:\Users\brgoz\OneDrive\Projects\Week7Lab\Week7Lab\Seed\";
            var counter = 0;
            var imageUrls = new List<string>();
            imageUrls.AddRange(File.ReadLines(SeedPath + "postimageurls.csv"));
            var lipsums = new List<string>();
            lipsums.AddRange(File.ReadLines(SeedPath + "lipsums.csv"));
            List<ApplicationUser> users = context.Users.ToList();
            ;
            foreach (var usr in users)
            {
                while (usr.MyPosts.Count < countPerUser)
                {
                    var newPost = new Post
                    {
                        Id = Guid.NewGuid(),
                        Author = usr,
                        Description = lipsums.OrderBy(x => Guid.NewGuid()).FirstOrDefault(),
                        Image = Util.GetImageBytes(imageUrls.OrderBy(x => Guid.NewGuid()).FirstOrDefault()),
                        CreatedOn = DateTime.Now
                    };
                    context.Posts.Add(newPost);
                    counter++;
                }
                context.SaveChanges();
            }
            return counter;
        }


        public static void tmp(ApplicationDbContext context)
        {
            string filename = @"C:\Users\brgoz\OneDrive\Projects\Week7Lab\Week7Lab\Seed\postimageurls.csv";
            var imageUrls = System.IO.File.ReadLines(filename);
            List<byte[]> images = new List<byte[]>();
            foreach (string url in imageUrls)
            {
                images.Add(Util.GetImageBytes(url));
            }
            foreach (var p in context.Posts)
            {
                p.Image = images.OrderBy(x => Guid.NewGuid()).First();
            }
            context.SaveChanges();
        }
        public static new void Seed(ApplicationDbContext context)
        {
            var usersSeeded = SeedUsers(context);
            var postsSeeded = SeedPosts(context);
        }
    }
}

