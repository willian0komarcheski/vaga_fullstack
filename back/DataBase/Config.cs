using Microsoft.EntityFrameworkCore;

public class Config : DbContext
{
    public Config(DbContextOptions<Config> options) : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Livro> Livros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Livro>()
            .HasMany(l => l.Categorias)
            .WithOne(c => c.Livro)
            .HasForeignKey(c => c.LivroId)
            .IsRequired();
    }
}
