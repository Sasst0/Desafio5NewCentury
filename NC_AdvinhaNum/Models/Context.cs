using Microsoft.EntityFrameworkCore;

namespace NC_AdvinhaNum.Models
{
 public class Contexto : DbContext
    {

        public DbSet<AdvinhaNumerocs> AdvinhaNC { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes): base(opcoes)
        {

        }
    }
}
