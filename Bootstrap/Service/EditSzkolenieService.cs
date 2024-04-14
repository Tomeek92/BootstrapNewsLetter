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
