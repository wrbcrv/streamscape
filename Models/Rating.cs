using System.ComponentModel;

namespace Api.Models
{
    public enum Rating
    {
        [Description("Livre")]
        L,

        [Description("10")]
        Age10,

        [Description("12")]
        Age12,

        [Description("14")]
        Age14,

        [Description("16")]
        Age16,

        [Description("18")]
        Age18
    }
}