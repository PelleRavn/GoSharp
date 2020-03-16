using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoSharp.DbContexts;
using GoSharp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GoSharp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet(string code)
        {
            using (var db = new GoContext())
            {
                if (string.IsNullOrEmpty(code))
                {
                    return NoCode(db);
                }

                code = code.ToLower();

                return GoToUrl(code, db);
            }
        }

        private IActionResult GoToUrl(string code, GoContext db)
        {
            var link = db.Links.FirstOrDefault(f => f.Code.Equals(code));
            if (link == null)
            {
                return GoToEditPage(code);
            }

            link.VisitCount++;
            db.SaveChanges();

            return Redirect(link.Url);
        }

        private IActionResult NoCode(GoContext db)
        {
            string code = null;
            while (code == null)
            {
                var newCode = RandomGenerator.RandomString();
                if (!db.Links.Any(a => a.Code.Equals(newCode)))
                {
                    code = newCode;
                }
            }

            return GoToEditPage(code);
        }

        private IActionResult GoToEditPage(string code)
        {
            return Redirect($"/edit/{code}");
        }
    }
}
