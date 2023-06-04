using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.UI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}