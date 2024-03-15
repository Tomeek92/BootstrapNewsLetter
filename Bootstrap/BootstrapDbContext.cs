using Bootstrap.Models.Admin;
using Bootstrap.Models.UsersEmail;
using Microsoft.EntityFrameworkCore;

namespace Bootstrap
{
    public class BootstrapDbContext : DbContext
    {
        public BootstrapDbContext(DbContextOptions<BootstrapDbContext> options) : base(options)
        {

        }
        public DbSet<UsersEmailModel> Users { get; set; }
        public DbSet<AccountAdmin> AccountAdmins { get; set; }
    }
}
