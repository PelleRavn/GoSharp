using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoSharp.DbContexts;
using GoSharp.Models;
using GoSharp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GoSharp.Pages
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;

        public string Address { get; set; }

        public EditModel(ILogger<EditModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Redirect("/");
            }

            code = code.ToLower();

            using (var db = new GoContext())
            {
                var link = db.Links.FirstOrDefault(f => f.Code.Equals(code));
                if (link != null)
                {
                    Address = link.Url;
                }
            }

            return Page();
        }

        public IActionResult OnPost(string code, string address)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Redirect("/");
            }

            if (string.IsNullOrEmpty(address))
            {
                return Redirect($"/edit/{code}");
            }

            if (!UrlChecker.CheckUrl(address))
            {
                return Redirect($"/edit/{code}");
            }

            code = code.ToLower();

            using (var db = new GoContext())
            {
                var link = db.Links.FirstOrDefault(f => f.Code.Equals(code));
                if (link == null)
                {
                    link = new Link();
                    link.Code = code;

                    db.Links.Add(link);
                }

                link.Url = address;
                db.SaveChanges();

                Address = address;
            }

            return Page();
        }
    }
}
