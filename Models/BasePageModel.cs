using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoSharp.Models
{
    public class BasePageModel : PageModel
    {
        public void SetErrorMessage(string errorMessage)
        {
            ViewData["ErrorMessage"] = errorMessage;
        }

        public void SetSuccessMessage(string successMessage)
        {
            ViewData["SuccessMessage"] = successMessage;
        }
    }
}
