using Microsoft.EntityFrameworkCore;
using KutuphaneYonetim.Models;

namespace KutuphaneYonetim.Data
{
    public class KutuphaneYonetimContext : DbContext
    {
        public KutuphaneYonetimContext(DbContextOptions<KutuphaneYonetimContext> options)
            : base(options)
        {
        }

        // Buradaki isme "Kitaplar" dedim.
        public DbSet<Kitap> Kitaplar { get; set; }
    }
}