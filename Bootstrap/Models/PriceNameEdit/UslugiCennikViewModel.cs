

using Bootstrap.Models.PriceNameEdit;
using Microsoft.Identity.Client;

namespace Bootstrap.Models.UslugiCennikViewModel
{
    public class UslugiCennikViewModel
    {
        public List<UslugiCennikModel> ListaUslug { get; set; }
        public UslugiCennikViewModel()
        {
            ListaUslug = new List<UslugiCennikModel>();
            {
                new UslugiCennikModel { Id = 1, Name = "Karnet miesięczny", Price = "Cena: 100 zł" };
                new UslugiCennikModel { Id = 2, Name = "Karnet roczny", Price = "Cena: 1000 zł / rok" };
                new UslugiCennikModel { Id = 3, Name = "Konsultacje trenera", Price = "Cena: W cenie karnetu" };
                new UslugiCennikModel { Id = 4, Name = "Karnet miesięczny 2", Price = "Cena: 100 zł / miesiąc" };
                new UslugiCennikModel { Id = 5, Name = "Karnet roczny 2", Price = "Cena: 1000 zł / rok" };
                new UslugiCennikModel { Id = 6, Name = "Konsultacje trenera", Price = "Cena: W cenie KARNETU TRALALASLASLA " };
                new UslugiCennikModel { Id = 7, Name = "Usługa 7", Price = "" };
                new UslugiCennikModel { Id = 8, Name = "Usługa 8", Price = "" };
                new UslugiCennikModel { Id = 9, Name = "Usługa 9", Price = "" };
                new UslugiCennikModel { Id = 10, Name = "Usługa 10", Price = "" };
            }

        }
        public void ZmienCene(int idUslugi, string newPrice)
        {
            var usluga = ListaUslug.FirstOrDefault(u => u.Id == idUslugi);
            if (usluga != null)
            {
                usluga.Price = newPrice;
            }
        }
        public void ZmienNazwe(int idUslugi, string newName)
        {
            var usluga = ListaUslug.FirstOrDefault(u => u.Id == idUslugi);
            if (usluga != null)
            {
                usluga.Name = newName;
            }
        }
    }
}
