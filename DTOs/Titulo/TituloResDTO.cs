namespace api.DTOs.Titulo
{
    public class TituloResDTO
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Sinopse { get; set; }
        public int Lancamento { get; set; }

        public static TituloResDTO valueOf(Models.Titulo titulo)
        {
            if (titulo == null)
                return null;

            return new TituloResDTO
            {
                Id = titulo.Id,
                Titulo = titulo.TituloStr,
                Sinopse = titulo.Sinopse,
                Lancamento = titulo.Lancamento
            };
        }
    }
}