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
        public IActionResult EditSzkol(List<SzkoleniaModel> editSzkolenie)
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
                throw new Exception("Nieoczekiwany błąd podczas pobierania listy usług" + ex.Message);
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
                return RedirectToAction("EditSzkol", "EditSzkolenia");
            }
            catch (Exception ex)
            {
                throw new Exception("Nie udało się usunąć" + ex);
            }
        }
        public IActionResult Delete()
        {
            return View();
        }

    }
}
