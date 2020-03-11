using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    [Authorize]
    public class CreateModel : PageModel
    {

        //access the database privately
        private readonly ApplicationDbContext _db;


        //Solution for not initialized methods  in create handlers
        [BindProperty]
        public Car Car { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //initialize the database
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
         

        //this is for index viewing,  passing and id. Null if this is not an admin usaer

        public IActionResult OnGet(string userId=null)
        {
            //initialize varible
            Car = new Car();
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;

            }

            Car.UserId = userId;
            return Page();
        }

        //creating page, onpost sync is handler

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Car.Add(Car);
            await _db.SaveChangesAsync();
            StatusMessage = "Vehicle has been added Successfully";

            return RedirectToPage("Index", new{userId = Car.UserId});
            
          
        }
    }
}