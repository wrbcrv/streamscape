using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Titulos")]
    public class Titulo
    {
        public int Id { get; set; }
        public string TituloStr { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
        public int Lancamento { get; set; }
        public List<Genero> Generos { get; set; }
        public Classificacao Classificacao { get; set; }
        public string Thumb { get; set; } = string.Empty;
        public string Banner { get; set; } = string.Empty;
    }
}