using MvcApiEventosExamen.Models;
using System.Text.Json.Serialization;

namespace MvcApiEventosExamen.Models
{
    public class EventoModel
    {
        [JsonPropertyName("idEvento")]
        public int IdEvento { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("artista")]
        public string Artista { get; set; }

        [JsonPropertyName("idCategoria")]
        public int IdCategoria { get; set; }

        [JsonPropertyName("imagen")]
        public string Imagen { get; set; }

        [JsonPropertyName("categoria")]
        public CategoriaEventoModel Categoria { get; set; }
    }
}