public class Categoria
{
    public int Id { get; set; }
    public string Tipo { get; set; }
    public int LivroId { get; set; }
    public Livro Livro { get; set; }
}
