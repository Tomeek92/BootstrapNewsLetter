using Bootstrap.Models.PriceNameEdit;
using Bootstrap.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Bootstrap.Controllers
{
    public class EditSzkolenia : Controller
    {
        private readonly IEditSzkolenie _szkolenie;
        private readonly BootstrapDbContext _db;
        public EditSzkolenia(IEditSzkolenie szkolenie, BootstrapDbContext db)
        {
            _szkolenie = szkolenie;
            _db = db;
        }
        [HttpPost]
        [Authorize]
        public IActionResult EditSzkol(List<SzkoleniaModel>editSzkolenie)
        {
            _szkolenie.Edit(editSzkolenie);
            return View(editSzkolenie);
        }
        [HttpGet]
        [Authorize]
        public IActionResult EditSzkol()
        {
            try
            {
                var uslugi = _db.SzkoleniaModels.ToList();
                return View(uslugi);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ViewBag.Error = "Nieoczekiwany błąd podczas pobierania listy usług";

                return View(new List<SzkoleniaModel>());
            }
        }
        [HttpPost]
        [Authorize]
        public IActionResult Add(SzkoleniaModel szkolenia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.SzkoleniaModels.Add(szkolenia);
                    _db.SaveChanges();

                    ViewBag.Success = "Dodano nowe Szkolenie!";
                    return RedirectToAction("EditSzkol", "EditSzkolenia");
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
            return View(szkolenia);
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
                var elementDoUsuniecia = _db.SzkoleniaModels.Find(Id);

                if (elementDoUsuniecia == null)
                {
                    return NotFound();
                }

                _db.SzkoleniaModels.Remove(elementDoUsuniecia);
                _db.SaveChanges();

                ViewBag.Success = "Usunięto!";

                return RedirectToAction("EditCennik", "EditCennik");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Error = "Nie udało się usunąć!";
                return View("EditSzkol");
            }
        }
        public IActionResult Delete()
        {
            return View();
        }

    }
}
