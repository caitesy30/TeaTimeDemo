using Microsoft.EntityFrameworkCore;

namespace TeaTimeDemo.Data
{
    public class ApplicationDbContext:DbContext
    {
        public
            ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
