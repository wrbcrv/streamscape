using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Models;

namespace api.DTOs.Titulo
{
    public class TituloResDTO
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Sinopse { get; set; }
        public int Lancamento { get; set; }
        public List<string>? Generos { get; set; }
        public string? Classificacao { get; set; }
        public string? Thumb { get; set; }
        public string? Banner { get; set; }

        public static TituloResDTO? ValueOf(Models.Titulo titulo)
        {
            if (titulo == null)
            {
                return null;
            }

            List<string> generos = new List<string>();

            foreach (Genero genero in titulo.Generos)
            {
                var genDisplayAttr = typeof(Genero)?.GetField(genero.ToString())?.GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute;
                generos.Add(genDisplayAttr.Name);
            }

            var classifDisplayAttr = typeof(Classificacao)?.GetField(titulo.Classificacao.ToString())?.GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute;
            var classifDisplay = classifDisplayAttr?.Name;

            return new TituloResDTO
            {
                Id = titulo.Id,
                Titulo = titulo.TituloStr,
                Sinopse = titulo.Sinopse,
                Lancamento = titulo.Lancamento,
                Generos = generos,
                Classificacao = classifDisplay,
                Thumb = titulo.Thumb,
                Banner = titulo.Banner
            };
        }
    }
}