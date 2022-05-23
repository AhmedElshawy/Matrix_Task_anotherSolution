using Microsoft.EntityFrameworkCore;
using Matrix_Task_anotherSolution.Models;

namespace Matrix_Task_anotherSolution.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategoryProp>().HasKey(c => new { c.ProductPropId, c.CategoryPropId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Matrix_Task_anotherSolution.Models.CategoryProps> CategoryProps { get; set; }

        public DbSet<Matrix_Task_anotherSolution.Models.Product> Product { get; set; }

        public DbSet<Matrix_Task_anotherSolution.Models.ProductProp> ProductProp { get; set; }
        public DbSet<Matrix_Task_anotherSolution.Models.ProductCategoryProp> ProductCategoryProps { get; set; }
    }
}
