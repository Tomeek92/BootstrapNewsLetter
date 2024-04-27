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
        public List<string> ShowUsersEmail()
        {
            
            var users = _context.Users.ToList();

           
            List<string> emails = new List<string>();

          
            foreach (var user in users)
            {
                emails.Add(user.Email); 
            }

          
            return emails;
        }
        public void DeleteEmailFromDb(UsersEmailModel userEmail)
        {

                _context.Users.Remove(userEmail);
                _context.SaveChanges();
        }
    }
}
