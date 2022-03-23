using System.ComponentModel.DataAnnotations;

namespace RevendaVeiculos.Data.Enums
{
    public enum StatusVeiculoEnum
    {
        [Display(Name = "Indisponível")]
        Indisponivel = 0,

        [Display(Name = "Disponível")]
        Disponivel = 1,

        [Display(Name = "Vendido")]
        Vendido = 2
    }
}
