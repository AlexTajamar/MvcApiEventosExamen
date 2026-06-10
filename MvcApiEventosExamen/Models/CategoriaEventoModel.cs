using System.Text.Json.Serialization;

namespace MvcApiEventosExamen.Models
{
    public class CategoriaEventoModel
    {
 
        [JsonPropertyName("idCategoria")]
        public int IdCategoria { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
    }
}