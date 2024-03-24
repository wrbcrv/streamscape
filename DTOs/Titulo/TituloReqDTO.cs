namespace api.DTOs.Titulo
{
    public class TituloReqDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
        public int Lancamento { get; set; }
        public List<string> Generos { get; set; } = new List<string>();
        public string ThumbPath { get; set; } = string.Empty;
    }
}