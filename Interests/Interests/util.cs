using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Interests.Models;

namespace Interests
{
    public static class Util
    {
        static readonly ApplicationDbContext Db = new ApplicationDbContext();

        public static byte[] GetImageBytes(string url)
        {
            var client = new WebClient();
            var imageArray = client.DownloadData(url);

            return ResizeImage(imageArray, 100, 100);
        }

        //}
        //public static byte[] GetUserImageBytes(Guid userId)
        //{
        //    var imageArray = Db.Users.Find(userId).Image;
        //    return ResizeImage(imageArray, 100, 100);
        //}

        public static byte[] GetPostImageBytes(Guid postId)
        {
            var imageArray = Db.Posts.Find(postId).Image;
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


    }
}