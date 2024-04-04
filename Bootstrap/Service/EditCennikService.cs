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
                    Console.WriteLine("Nieoczekiwany błąd");
                }
            }
            try
            {
                _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }
    }
}
