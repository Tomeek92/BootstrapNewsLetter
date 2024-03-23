using Bootstrap.Models.PriceNameEdit;
using Microsoft.EntityFrameworkCore;

namespace Bootstrap.Seeder
{
    public class Seeder
    {
        private readonly BootstrapDbContext _db;
        public Seeder(BootstrapDbContext db)
        {
            _db = db;

        }
        
        public async Task Seed()
        {
            if (await _db.Database.CanConnectAsync())
            {
                var uslugi = new List<UslugiCennikModel>()
                {
                    new UslugiCennikModel {Name = "Karnet miesięczny", Price = "Cena: 100 zł" },
                    new UslugiCennikModel {Name = "Karnet roczny", Price = "Cena: 1000 zł / rok" },
                    new UslugiCennikModel {Name = "Konsultacje trenera", Price = "Cena: W cenie karnetu" },
                    new UslugiCennikModel {Name = "Karnet miesięczny 2", Price = "Cena: 100 zł / miesiąc" },
                    new UslugiCennikModel {Name = "Karnet roczny 2", Price = "Cena: 1000 zł / rok" },
                    new UslugiCennikModel {Name = "Konsultacje trenera", Price = "Cena: W cenie KARNETU TRALALASLASLA " },
                };
                foreach (var usluga in uslugi)
                {
                    // Sprawdź, czy usługa już istnieje
                    var existingUsluga = _db.UslugiCennikModels.FirstOrDefault(u => u.Name == usluga.Name);

                    if (existingUsluga == null)
                    {
                        // Jeśli usługa nie istnieje, dodaj ją do bazy danych
                        _db.UslugiCennikModels.Add(usluga);
                    }
                }
                await _db.SaveChangesAsync();
            }
        }
    }
}
        
    

