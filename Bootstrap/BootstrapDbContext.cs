using Bootstrap.Models.Admin;
using Bootstrap.Models.PriceNameEdit;
using Bootstrap.Models.UsersEmail;
using Bootstrap.Models.UslugiCennikViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Bootstrap
{
    public class BootstrapDbContext : IdentityDbContext<AccountAdmin>
    {
        public BootstrapDbContext(DbContextOptions<BootstrapDbContext> options) : base(options)
        {

        }
        public DbSet<UsersEmailModel> Users { get; set; }
        public DbSet<AccountAdmin> AccountAdmins { get; set; }
        public DbSet<UslugiCennikModel> UslugiCennikModels { get; set; }

        
    }
    
}
