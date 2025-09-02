using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class AppDbAtbContext : DbContext
{
    public AppDbAtbContext(DbContextOptions<AppDbAtbContext> options) : base(options) { }
    public DbSet<CategoryEntity> Categories { get; set; }
}
