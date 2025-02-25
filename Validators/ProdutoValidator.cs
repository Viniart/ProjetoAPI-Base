using FluentValidation;
using ProjetoAPI.Model;

namespace ProjetoAPI.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.Categoria)
                .NotEmpty().WithMessage("Informe uma Categoria")
                .Length(3, 100).WithMessage("Categoria deve ter no minimo 3 e no máximo 100 caracteres");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe o Nome do Produto")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres");
        }
    }
}
