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
        public byte[] Image { get; set; }
        public string LinkUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}