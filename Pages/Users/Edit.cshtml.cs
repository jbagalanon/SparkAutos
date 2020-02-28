using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }


        //Lambda error if this bindproperty is 
        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }


        //original information is this  public void OnGet()

        public async Task<IActionResult> OnGetAsync(string id)
        {
            //if this await string, the original code which is   if (id == null) must change
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var applicationUserFromDb =
                    await _db.ApplicationUser.SingleOrDefaultAsync(u => u.Id == ApplicationUser.Id);

                if (applicationUserFromDb == null)
                {
                    return NotFound();
                }
                else

                {
                    applicationUserFromDb.Name = ApplicationUser.Name;
                    applicationUserFromDb.PhoneNumber = ApplicationUser.PhoneNumber;
                    applicationUserFromDb.Address = ApplicationUser.Address;
                    applicationUserFromDb.City = ApplicationUser.City;
                    applicationUserFromDb.PostalCode = ApplicationUser.PostalCode;

                    await _db.SaveChangesAsync();

                    return RedirectToPage("Index");
                }
            }

        }

    }
}