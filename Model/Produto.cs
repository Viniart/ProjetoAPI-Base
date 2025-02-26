
namespace ProjetoAPI.Model
{
    public class Produto
    {
        // Construtor - Define o que a classe tem que fazer quando for instanciada
        public Produto()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
