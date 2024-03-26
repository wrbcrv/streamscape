using api.Models;

namespace api.DTOs.Titulo
{
    public class TituloReqDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
        public int Lancamento { get; set; }
        public List<Genero> Generos { get; set; }
        public Classificacao Classificacao { get; set; }
    }
}