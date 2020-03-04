using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Utility;

namespace SparkAuto.Pages.ServiceTypes
{

    [Authorize(Roles = SD.AdminEndUser)]
    public class CreateModel : PageModel
    {
        
        //access the database privately
        private  readonly ApplicationDbContext _db;
        //initialize the database
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //Solution for not initialized methods  in create handlers
        [BindProperty]
        public ServiceType ServiceType { get; set; }

        //this is for index viewing, 
       
        public IActionResult OnGet()
        {
            return Page();
        }

        //creating page, onpost sync is handler

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.ServiceType.Add(ServiceType);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
