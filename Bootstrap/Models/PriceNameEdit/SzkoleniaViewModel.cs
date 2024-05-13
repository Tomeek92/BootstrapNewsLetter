using Bootstrap.Models.PriceNameEdit;

namespace Bootstrap.Models.SzkoleniaViewModel
{
    public class SzkoleniaViewModel
    {
        public List<SzkoleniaModel> SzkoleniaList { get; set; }
        public SzkoleniaViewModel()
        {
            SzkoleniaList = new List<SzkoleniaModel>();
            {
                new SzkoleniaModel { Id = 1, Name = "Szkolenie1", Price = "123zł", Category = "Szkolenia" };
                new SzkoleniaModel { Id = 2, Name = "Szkolenia2", Price = "123zł", Category = "Szkolenia" };
            }
        }
        public void ZmienCene(int idUslugi, string newPrice)
        {
            var usluga = SzkoleniaList.FirstOrDefault(u => u.Id == idUslugi);
            if (usluga != null)
            {
                usluga.Price = newPrice;
            }
        }
        public void ZmienNazwe(int idUslugi, string newName)
        {
            var usluga = SzkoleniaList.FirstOrDefault(u => u.Id == idUslugi);
            if (usluga != null)
            {
                usluga.Name = newName;
            }
        }
    }
}

