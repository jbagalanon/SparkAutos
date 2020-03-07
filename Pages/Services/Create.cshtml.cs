using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Models.ViewModel;

namespace SparkAuto.Pages.Services
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {

            _db = db;

        }

        [BindProperty]
        public CarServiceViewModel CarServiceVM { get; set; }


        //this is for the car service model
        public async Task<IActionResult> OnGet(int carId)
        {
            CarServiceVM = new CarServiceViewModel()
            {
                Car = await _db.Car.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == carId),
                ServiceHeader = new Models.ServiceHeader()
            };
            List<string> lstServiceTypeInShoppingCard = _db.ServiceShoppingCart
                .Include(c => c.ServiceType)
                .Where(c => c.CarId == carId)
                .Select(s => s.ServiceType.Name)
                .ToList();

            IQueryable<ServiceType> lstService = from s in _db.ServiceType
                                                 where !(lstServiceTypeInShoppingCard.Contains(s.Name))
                                                 select s;

            CarServiceVM.ServiceTypeList = lstService.ToList();

            CarServiceVM.ServiceShoppingCart = _db.ServiceShoppingCart.Include(s => s.ServiceType)
                .Where(c => c.CarId == carId).ToList();

            CarServiceVM.ServiceHeader.TotalPrice = 0;

            foreach (var item in CarServiceVM.ServiceShoppingCart)
            {
                CarServiceVM.ServiceHeader.TotalPrice += item.ServiceType.Price;

            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                CarServiceVM.ServiceHeader.DateAdded = DateTime.Now;
                CarServiceVM.ServiceShoppingCart = _db.ServiceShoppingCart.Include(c => c.ServiceType)
                    .ToList();

                foreach (var item in CarServiceVM.ServiceShoppingCart)
                {
                    CarServiceVM.ServiceHeader.TotalPrice += item.ServiceType.Price;
                }

                CarServiceVM.ServiceHeader.CarId = CarServiceVM.Car.Id;

                _db.ServiceHeader.Add(CarServiceVM.ServiceHeader);
                await _db.SaveChangesAsync();

                foreach (var detail in CarServiceVM.ServiceShoppingCart)
                {
                    ServiceDetails serviceDetails = new ServiceDetails
                    {
                        ServiceHeaderId = CarServiceVM.ServiceHeader.Id,
                        ServiceName = detail.ServiceType.Name,
                        ServicePrice = detail.ServiceType.Price,
                        ServiceTypeId = detail.ServiceTypeId
                    };
                    _db.ServiceDetails.Add(serviceDetails);
                }

                _db.ServiceShoppingCart.RemoveRange(CarServiceVM.ServiceShoppingCart);

                await _db.SaveChangesAsync();
                return RedirectToPage("../Cars/Index", new {userId = CarServiceVM.Car.UserId});
            }

            return Page();


        }

        //handler to add the cart
        public async Task<IActionResult> OnPostAddToCart()
        {
            ServiceShoppingCart objServiceCart = new ServiceShoppingCart()
            {
                CarId = CarServiceVM.Car.Id,
                //binding the bakcgroun asp-for=servicetypeid
                ServiceTypeId = CarServiceVM.ServiceDetails.ServiceTypeId,

            };
            _db.ServiceShoppingCart.Add(objServiceCart);

            await _db.SaveChangesAsync();

            return RedirectToPage("Create", new {carId = CarServiceVM.Car.Id});
        }
        //remove cart

        //handler to add the cart
        public async Task<IActionResult> OnPostRemoveFromCart(int serviceTypeId)
        {
            ServiceShoppingCart objServiceCart = _db.ServiceShoppingCart.FirstOrDefault
                (c => c.CarId == CarServiceVM.Car.Id && c.ServiceTypeId == serviceTypeId);
           
            _db.ServiceShoppingCart.Remove(objServiceCart);

            await _db.SaveChangesAsync();

            return RedirectToPage("Create", new { carId = CarServiceVM.Car.Id });
        }
    }
}