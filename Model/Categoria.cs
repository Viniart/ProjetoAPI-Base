namespace ProjetoAPI.Model
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        // OPCIONAL - Caso eu queira visualizar a lista de Produtos ao listar a categoria
        //public List<Produto> Produtos { get; } = new List<Produto>();
    }
}
