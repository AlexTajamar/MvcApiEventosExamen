
using System.Collections.Generic;
using System.Threading.Tasks;
using MvcApiEventosExamen.Models; // Modela las clases básicas en el MVC para recibir el JSON

namespace MvcApiEventosExamen.Services
{
    public interface IServicioConciertos
    {
        Task<List<CategoriaEventoModel>> GetCategoriasAsync();
        Task<List<EventoModel>> GetEventosAsync();
        Task<List<EventoModel>> GetEventosPorCategoriaAsync(int idCategoria);
    }
}