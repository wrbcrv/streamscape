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
    }
}