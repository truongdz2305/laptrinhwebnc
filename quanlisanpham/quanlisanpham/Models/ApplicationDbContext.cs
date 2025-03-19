using QuanLySanPham.Models;
using System.Data.Entity;

namespace quanlisanpham.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<SanPham> SanPhams { get; set; }
    }
}
