public class Livro
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public string FaixaEtaria { get; set; }
    public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
}
