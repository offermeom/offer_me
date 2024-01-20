using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Data;
public partial class OMContext : DbContext
{
    public OMContext() {}
    public OMContext(DbContextOptions<OMContext> options) : base(options) {}
    public virtual DbSet<User> Users { get; set; }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"));
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Offerme;Trusted_Connection=True;MultipleActiveResultSets=True;Integrated Security=True;TrustServerCertificate=True");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.ID);
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())").HasColumnType("datetime").HasColumnName("Created_at");
            entity.Property(e => e.GSTIN).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Mail).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Number).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(100).IsUnicode(false);
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}