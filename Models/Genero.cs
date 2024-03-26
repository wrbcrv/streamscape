using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public enum Genero
    {
        [Display(Name = "Ação")]
        Acao,
        [Display(Name = "Aventura")]
        Aventura,
        [Display(Name = "Comédia")]
        Comedia,
        [Display(Name = "Drama")]
        Drama,
        [Display(Name = "Terror")]
        Terror,
        [Display(Name = "Ficção Científica")]
        FiccaoCientifica,
        [Display(Name = "Romance")]
        Romance,
        [Display(Name = "Documentário")]
        Documentario,
        [Display(Name = "Animação")]
        Animacao,
        [Display(Name = "Suspense")]
        Suspense,
        [Display(Name = "Outro")]
        Outro
    }
}