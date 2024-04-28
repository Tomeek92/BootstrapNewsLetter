using Bootstrap.Models.PriceNameEdit;
using Bootstrap.Service.Interface;

namespace Bootstrap.Service
{
    public class EditSzkolenieService : IEditSzkolenie
    {
        private readonly BootstrapDbContext _context;
        public EditSzkolenieService(BootstrapDbContext dbContext)
        {
            _context = dbContext;
        }
        public void Edit(List<SzkoleniaModel> editSzkolenie)
        {
            try
            {
                foreach (var usluga in editSzkolenie)
                {
                    var dbUsluga = _context.UslugiCennikModels.FirstOrDefault(u => u.Id == usluga.Id);
                    if (dbUsluga != null)
                    {
                        dbUsluga.Price = usluga.Price;
                        dbUsluga.Name = usluga.Name;
                        dbUsluga.Category = usluga.Category;
                    }
                    else
                    {
                        throw new Exception("Nieoczekiwany błąd: Nie znaleziono usługi o ID: " + usluga.Id);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas zapisu do bazy danych: " + ex.Message);
            }
        }
    }
}
