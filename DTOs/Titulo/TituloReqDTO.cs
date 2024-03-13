namespace api.DTOs.Titulo
{
    public class TituloReqDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
        public int Lancamento { get; set; }
        public string ThumbPath { get; set; } = string.Empty;
    }
}