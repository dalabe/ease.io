using Microsoft.EntityFrameworkCore;

namespace Ease.Data
{
    public class DataContext(DbContextOptions<DataContext> options): DbContext(options)
    {
        public DbSet<EasyMetadata> Metadata { get; set; }
    }
}
