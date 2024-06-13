using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public enum Type
    {
        [Description("Ação e Aventura")]
        AcaoAventura,

        [Description("Anime")]
        Anime,

        [Description("Crianças e Família")]
        CriancasFamilia,

        [Description("Comédias")]
        Comedias,

        [Description("Crime")]
        Crime,

        [Description("Documentários")]
        Documentarios,

        [Description("Dramas")]
        Dramas,

        [Description("Horror")]
        Terror,

        [Description("Independente")]
        Independente,

        [Description("Ficção Científica e Fantasia")]
        FiccaoCientificaFantasia
    }
}