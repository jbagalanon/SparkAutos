using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Models.ViewModel;
using SparkAuto.Utility;


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
        //the original file was change  public List<ApplicationUser> ApplicationUserList after moving it to viewmodel
        [BindProperty]
        public UsersListViewModel UsersListVM { get; set; }


        //this is the default value  public void OnGet()
        //onget is use to retrieve data in database

        //must receive a product
        public async Task<IActionResult> OnGet(int productPage=1, string searchEmail=null, string searchName=null, string searchPhone=null)
        {
            UsersListVM = new UsersListViewModel()
            {
                ApplicationUserList = await _db.ApplicationUser.ToListAsync()
            };

            //register string value from pagelinktaghelpers
            //i forgot the equal sign na naman
            StringBuilder param = new StringBuilder();
            param.Append("/Users?productPage=:");

            param.Append("$searchName=");

            //append the value in search
            if (searchName != null)
            {
                param.Append("$searchName=");
            }

            param.Append("$searchEmail=");

            //append the value in name
            if (searchEmail != null)
            {
                param.Append("$searchName=");
            }


            param.Append("$searchPhone=");

            //append the value in phone
            if (searchPhone != null)
            {
                param.Append("$searchPhone=");
            }

            if (searchEmail != null)
            {
                UsersListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.Email.ToLower()
                    .Contains(searchEmail.ToLower())).ToListAsync();
            }
            else
            {
                if (searchName != null)
                {
                    UsersListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.Name.ToLower()
                        .Contains(searchName.ToLower())).ToListAsync();
                }
                else
                {
                    if (searchPhone != null)
                    {
                        UsersListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.PhoneNumber.ToLower()
                            .Contains(searchPhone.ToLower())).ToListAsync();
                    }
                }
            }
           

            var count = UsersListVM.ApplicationUserList.Count;
            
            //remember joe, there is no semicolonin fucntion
            UsersListVM.PagingInfo =new PagingInfo()
            {
          
                //this information wont work if you dont add info in onget
                CurrentPage = productPage,
                ItemsPerPage = SD.PaginationUsersPageSize,
                TotalItems = count,
                UrlParam = param.ToString()
            }
            ;
            UsersListVM.ApplicationUserList = UsersListVM.ApplicationUserList.OrderBy(p => p.Email)
                .Skip((productPage - 1) * 2)
                .Take(SD.PaginationUsersPageSize).ToList();
                
            return Page();
        }


    }
}