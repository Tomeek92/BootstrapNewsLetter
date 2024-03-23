
using Bootstrap.Models.PriceNameEdit;
using Bootstrap.Models.UslugiCennikViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
    public class EditCennikController : Controller
    {
        private readonly BootstrapDbContext _bootstrapDbContext;
        public EditCennikController(BootstrapDbContext bootstrapDbContext)
        {
            _bootstrapDbContext = bootstrapDbContext;
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditCennik(List<UslugiCennikModel> editUslugi)
        {
            foreach (var usluga in editUslugi)
            {
                var dbUsluga = _bootstrapDbContext.UslugiCennikModels.FirstOrDefault(u => u.Id == usluga.Id);
                if (dbUsluga != null)
                {
                    dbUsluga.Price = usluga.Price;
                    dbUsluga.Name = usluga.Name;
                }
            }
            try
            {
                _bootstrapDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Obsłuż błąd
                // Możesz zalogować wyjątek lub przekazać informacje o błędzie do widoku
            }
            return View(editUslugi);
        }
        [HttpGet]
        [Authorize]
        public ActionResult EditCennik()
        {
            var uslugi = _bootstrapDbContext.UslugiCennikModels.ToList();
            return View(uslugi);
        }
    }
}
