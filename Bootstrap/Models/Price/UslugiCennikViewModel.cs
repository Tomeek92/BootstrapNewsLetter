using Microsoft.Identity.Client;

namespace Bootstrap.Models.Price
{
    public class UslugiCennikViewModel
    {
        public List<UslugiCennikModel> ListaUslug { get; set; }
        public UslugiCennikViewModel()
        {
            ListaUslug = new List<UslugiCennikModel>();
            {
                new UslugiCennikModel { Id = 1, Name = "Usługa 1", Price = 100.00m };
                new UslugiCennikModel { Id = 2, Name = "Usługa 2", Price = 200.00m };
                new UslugiCennikModel { Id = 3, Name = "Usługa 3", Price = 100.00m };
                new UslugiCennikModel { Id = 4, Name = "Usługa 4", Price = 200.00m };
                new UslugiCennikModel { Id = 5, Name = "Usługa 5", Price = 200.00m };
                new UslugiCennikModel { Id = 6, Name = "Usługa 6", Price = 200.00m };
                new UslugiCennikModel { Id = 7, Name = "Usługa 7", Price = 200.00m };
                new UslugiCennikModel { Id = 8, Name = "Usługa 8", Price = 200.00m };
                new UslugiCennikModel { Id = 9, Name = "Usługa 9", Price = 200.00m };
                new UslugiCennikModel { Id = 10, Name = "Usługa 10", Price = 200.00m };
            }
            
        }
        public void ZmienCene(int idUslugi , decimal newPrice)
        {
            var usluga = ListaUslug.FirstOrDefault(u => u.Id == idUslugi);
            if (usluga != null)
            {
                usluga.Price = newPrice;
            }
        }
    }
}
