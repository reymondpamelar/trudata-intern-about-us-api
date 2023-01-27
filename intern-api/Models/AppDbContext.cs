using Microsoft.EntityFrameworkCore;

namespace intern_api.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(){ }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public virtual DbSet<User?> users { get; set; }
    public virtual DbSet<Intern> interns { get; set; }
    public virtual DbSet<Contact> contact { get; set; }
}