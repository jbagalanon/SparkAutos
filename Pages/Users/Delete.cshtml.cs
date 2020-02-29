using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Utility;

namespace SparkAuto.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }


        //public void OnGet() always change this code to match your own
        public async  Task<IActionResult> OnGetAsync(string  id)
        {
            if (id.Trim().Length == 0)
            {
                return NotFound();
            }

            ApplicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == id);
            if (ApplicationUser == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            

            var UserInDb = await _db.ApplicationUser.SingleOrDefaultAsync(u =>u.Id==ApplicationUser.Id);

            if (UserInDb != null)
            {
                _db.ApplicationUser.Remove(UserInDb);
                await _db.SaveChangesAsync();
            }
            
            return RedirectToPage("Index");


            /*

            public async Task<IActionResult> OnPostAsync()
            {
                var userInDb = await _db.Users.SingleOrDefaultAsync(u => u.Id == ApplicationUser.Id);

                _db.Users.Remove(userInDb);
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            */
        }

        
    }
}