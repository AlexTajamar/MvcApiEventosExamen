using Microsoft.AspNetCore.Mvc;
using MvcApiEventosExamen.Models;
using MvcApiEventosExamen.Services;
using MvcApiEventosExamen.Models;
using MvcApiEventosExamen.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcApiEventosExamen.Controllers
{
    public class EventosController : Controller
    {
        private readonly IServicioConciertos _servicioConciertos;

        public EventosController(IServicioConciertos servicioConciertos)
        {
            _servicioConciertos = servicioConciertos;
        }

        public async Task<IActionResult> Index(int? idCategoria)
        {
            List<EventoModel> eventos;

            // COMPROBACIÓN: Validamos que no sea nulo, no esté vacío, y sea un ID numérico válido (> 0)
            if (idCategoria.HasValue && idCategoria.Value > 0)
            {
                eventos = await _servicioConciertos.GetEventosPorCategoriaAsync(idCategoria.Value);
                ViewData["CategoriaSeleccionada"] = idCategoria.Value;
            }
            else
            {
                // Si viene nulo, vacío o es inválido, mostramos el catálogo completo por seguridad
                eventos = await _servicioConciertos.GetEventosAsync();

                // Forzamos que la vista sepa que no hay categoría seleccionada
                ViewData["CategoriaSeleccionada"] = null;
            }

            // Obtenemos las categorías para pintar los botones de filtro
            List<CategoriaEventoModel> categorias = await _servicioConciertos.GetCategoriasAsync();

            // Comprobación extra de seguridad por si la API de categorías falla
            if (categorias == null)
            {
                categorias = new List<CategoriaEventoModel>();
            }

            ViewBag.Categorias = categorias;

            return View(eventos);
        }
    }
}