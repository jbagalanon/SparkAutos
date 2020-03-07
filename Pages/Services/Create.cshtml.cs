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

        public async  Task<IActionResult> OnGet( int carId)
        {
            CarServiceVM = new CarServiceViewModel()
            {
                Car = await _db.Car.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == carId),
                ServiceHeader =  new Models.ServiceHeader()
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
    }
}