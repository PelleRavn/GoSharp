using System;
using System.Linq;
using GoSharp.DbContexts;
using GoSharp.Models;
using GoSharp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoSharp.Pages
{
    public class EditModel : BasePageModel
    {
        private readonly ILogger<EditModel> _logger;

        public string Address { get; set; }
        public string Code { get; set; }

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

            if (code.Equals("links", StringComparison.OrdinalIgnoreCase))
            {
                return Redirect("/links");
            }

            code = code.ToLower();
            Initialize(code);

            return Page();
        }

        private void Initialize(string code)
        {
            Code = code;
            using (var db = new GoContext())
            {
                var link = db.Links.FirstOrDefault(f => f.Code.Equals(code));
                if (link != null)
                {
                    Address = link.Url;
                }
            }
        }

        public IActionResult OnPost(string code, string address)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Redirect("/");
            }

            if (code.Equals("links", StringComparison.OrdinalIgnoreCase))
            {
                SetErrorMessage("Short address can't be 'links'. It is reserved!");
                return Page();
            }

            code = code.ToLower();

            Initialize(code);

            if (string.IsNullOrEmpty(address))
            {
                SetErrorMessage("You need to enter an address!");
                return Page();
            }

            if (!UrlChecker.CheckUrl(address))
            {
                SetErrorMessage("You need to enter a full valid address!");

                return Page();
            }

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
                link.UpdatedAt = DateTime.UtcNow;

                db.SaveChanges();

                Address = address;

                SetSuccessMessage("Your short link was saved.");
            }

            return Page();
        }
    }
}
