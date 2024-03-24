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
        [Display(Name = "Ficção Científica")]
        FiccaoCientifica,
        [Display(Name = "Terror")]
        Terror,
        [Display(Name = "Romance")]
        Romance,
        [Display(Name = "Documentário")]
        Documentario,
        [Display(Name = "Anime")]
        Anime,
        [Display(Name = "Outro")]
        Outro
    }
}