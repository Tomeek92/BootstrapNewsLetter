using Bootstrap.Models.UsersEmail;
using Bootstrap.Service.Interface;

namespace Bootstrap.Service
{
    public class SaveEmailToDbService : ISaveEmailToDbService
    {
        private readonly BootstrapDbContext _context;
        public SaveEmailToDbService(BootstrapDbContext context)
        {
            _context = context;
        }
        public void SaveEmailToDb(UsersEmailModel usersEmail)
        {
            _context.Add(usersEmail);
            _context.SaveChanges();
            
        }
    }
}
