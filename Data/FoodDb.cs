using Microsoft.EntityFrameworkCore;
using BackendAPI.Models;

namespace BackendAPI.Data
{
    public class FoodDb : DbContext
    {
        public FoodDb(DbContextOptions<FoodDb> options) : base(options) { }
        public DbSet<Food> FoodList { get; set; } = null!;
    }
            
}
