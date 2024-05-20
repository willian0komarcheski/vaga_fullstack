using Microsoft.EntityFrameworkCore;

public class Config : DbContext
{
    public Config(DbContextOptions<Config> options) : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Livro> Livros { get; set; }

}
