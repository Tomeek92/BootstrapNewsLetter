using Bootstrap.Models.UsersEmail;

namespace Bootstrap.Service.Interface
{
    public interface ISaveEmailToDbService
    {
        public void SaveEmailToDb(UsersEmailModel usersEmail);
    }
}
