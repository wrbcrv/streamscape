using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public enum Classificacao
    {
        [Display(Name = "Livre")]
        Livre,
        [Display(Name = "10")]
        DezAnos,
        [Display(Name = "12")]
        DozeAnos,
        [Display(Name = "14")]
        QuatorzeAnos,
        [Display(Name = "16")]
        DezesseisAnos,
        [Display(Name = "18")]
        DezoitoAnos
    }
}