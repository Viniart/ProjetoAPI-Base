using FluentValidation;
using ProjetoAPI.Model;

namespace ProjetoAPI.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.CategoriaId)
                .NotEmpty().WithMessage("Informe uma Categoria");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe o Nome do Produto")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres");
        }
    }
}
