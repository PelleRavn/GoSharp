using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoSharp.DbContexts;
using GoSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoSharp.Pages
{
    public class LinksModel : PageModel
    {
        public List<Link> Links { get; set; }

        public void OnGet()
        {
            using (var db = new GoContext())
            {
                Links = db.Links.ToList();
            }
        }
    }
}
