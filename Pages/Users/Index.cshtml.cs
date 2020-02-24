using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;


//always fix this namespace, it must be the directory of the page
namespace SparkAuto.Pages.Users
{
    public class IndexModel : PageModel
    {

        //we need to retrieve the user, so we should call the database

        private readonly ApplicationDbContext _db;


        //create this constructor once the database initialized 
        public IndexModel( ApplicationDbContext db)
        {
            _db = db;
        }

        //bindproperty is used to bind the userlist
        //application user is the model created recently. it is located in models folder    
        [BindProperty]
        public List<ApplicationUser> ApplicationUserList { get; set; }


        //this is the default value  public void OnGet()
        //onget is use to retrieve data in database
        public async  Task<IActionResult>OnGet()
        {
            ApplicationUserList = await _db.ApplicationUser.ToListAsync();
            return Page();
        }
    }
}