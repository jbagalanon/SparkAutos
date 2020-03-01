using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;

namespace SparkAuto.Areas.Identity.Pages.Account
{
    public class VerifyEmailModel : PageModel
    {

       

        public  string Email { get; set; }
        public IActionResult OnGet(string id)
        {
            Email = id;
            return Page();
        }



        
        //Add post handler OnPost Asynch



        /*
         * var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { area = "Identity", userId = user.Id, code = code },
                                protocol: Request.Scheme);

                            await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
         *
         */

    }
}
