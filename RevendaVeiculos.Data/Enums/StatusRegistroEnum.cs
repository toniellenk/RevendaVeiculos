using System.ComponentModel.DataAnnotations;

namespace RevendaVeiculos.Data.Enums
{
    public enum StatusRegistroEnum
    {
        [Display(Name = "Cancelado")]
        Cancelado = 0,
        [Display(Name = "Ativo")]
        Ativo = 1
    }
}
