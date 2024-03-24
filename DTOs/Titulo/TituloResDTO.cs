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
        public List<string> Generos { get; set; } = new List<string>();
        public string? ThumbPath { get; set; }

        public static TituloResDTO ValueOf(Models.Titulo titulo)
        {
            if (titulo == null)
                return null;

            var displayed = new List<string>();
            foreach (var genero in titulo.Generos)
            {
                var display = typeof(Genero).GetField(genero.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute;
                displayed.Add(display.Name);
            }

            return new TituloResDTO
            {
                Id = titulo.Id,
                Titulo = titulo.TituloStr,
                Sinopse = titulo.Sinopse,
                Lancamento = titulo.Lancamento,
                Generos = displayed,
                ThumbPath = titulo.ThumbPath,
            };
        }
    }
}
