using Bootstrap.Models.PriceNameEdit;
using Bootstrap.Service.Interface;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bootstrap.Service
{
    public class EditCennikService : IEditCennik
    {
        private readonly BootstrapDbContext _context;
        public EditCennikService(BootstrapDbContext context)
        {
            _context = context;
        }
        public void Edit(List<UslugiCennikModel> editUslugi)
        {
            try
            {
                foreach (var usluga in editUslugi)
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
