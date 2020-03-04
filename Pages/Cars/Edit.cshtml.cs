using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public  Car Car { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Car = await _db.Car.FirstOrDefaultAsync(c => c.Id == id);

            if (Car == null)
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

            //_db.Attach(ServiceType).State = EntityState.Modified;

            var carFromDb = await _db.Car.FirstOrDefaultAsync(c => c.Id == Car.Id);
            carFromDb.VIN = Car.VIN;
            carFromDb.Make = Car.Make;
            carFromDb.Model = Car.Model;
            carFromDb.Style = Car.Style;
            carFromDb.Year = Car.Year;
            carFromDb.Color = Car.Color;

            await _db.SaveChangesAsync();
            /* this is used for default settings
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceTypeExists(ServiceType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
            return RedirectToPage("./Index");
        }


    }
}