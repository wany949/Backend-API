using Microsoft.EntityFrameworkCore;
using BackendAPI.Models;

namespace BackendAPI.Data
{
    public class CharacterDb : DbContext
    {
        public CharacterDb(DbContextOptions<CharacterDb> options) : base(options) { }
        public DbSet<Character> Characters { get; set; } = null!;
    }
            
}
