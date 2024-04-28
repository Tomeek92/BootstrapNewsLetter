
using Bootstrap.Models.PriceNameEdit;
using Bootstrap.Models.UslugiCennikViewModel;
using Bootstrap.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Bootstrap.Controllers
{
    public class EditCennikController : Controller
    {
        private readonly BootstrapDbContext _bootstrapDbContext;
        private readonly IEditCennik _edit;
        public EditCennikController(BootstrapDbContext bootstrapDbContext, IEditCennik edit)
        {
            _bootstrapDbContext = bootstrapDbContext;
            _edit = edit;
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditCennik(List<UslugiCennikModel> editUslugi)
        {
            _edit.Edit(editUslugi);
            return View(editUslugi);
        }
           
        [HttpGet]
        [Authorize]
        public IActionResult EditCennik()
        {
            try
            {
                var uslugi =  _bootstrapDbContext.UslugiCennikModels.ToList();
                return View(uslugi);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ViewBag.Error = "Nieoczekiwany błąd podczas pobierania listy usług";

                return View(new List<UslugiCennikModel>());
            }
        }
        [HttpPost]
        [Authorize]
        public IActionResult Add(UslugiCennikModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bootstrapDbContext.UslugiCennikModels.Add(product);
                    _bootstrapDbContext.SaveChanges();
                    ViewBag.Success = "Dodano nową usługę!";
                    return RedirectToAction("EditCennik", "EditCennik");
                }
                else
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { x.Key, x.Value.Errors })
                        .ToArray();

                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(",", error.Errors.Select(e => e.ErrorMessage))}");
                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Nie można zapisać. Spróbuj ponownie, a jeśli problem będzie się powtarzał skontaktuj się z administratorem.");
            }
            return View(product);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            try
            {
                var elementDoUsuniecia = _bootstrapDbContext.UslugiCennikModels.Find(Id);
                if (elementDoUsuniecia == null)
                {
                    return NotFound();
                }

                _bootstrapDbContext.UslugiCennikModels.Remove(elementDoUsuniecia);
                _bootstrapDbContext.SaveChanges();

                ViewBag.Success = "Usunięto!";

                return RedirectToAction("EditCennik","EditCennik");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Error = "Nie udało się usunąć!";
                return View("EditCennik");
            }
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
