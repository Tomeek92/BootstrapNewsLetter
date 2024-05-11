using Bootstrap.Models.UsersEmail;

namespace Bootstrap.Service.Interface
{
    public interface ISaveEmailToDbService
    {
        public void SaveEmailToDb(UsersEmailModel usersEmail);
        List<string> ShowUsersEmail();
        public void DeleteEmailFromDb(UsersEmailModel usersEmail);
        public bool UserExisting(string userEmail);
    }
}
