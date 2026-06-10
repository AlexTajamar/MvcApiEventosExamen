using Microsoft.Extensions.Configuration;
using MvcApiEventosExamen.Models;
using MvcApiEventosExamen.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MvcApiEventosExamen.Services
{
    public class ServicioConciertos : IServicioConciertos
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ServicioConciertos(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiUrls:ConciertosApi");
        }

        public async Task<List<CategoriaEventoModel>> GetCategoriasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CategoriaEventoModel>>($"{_baseUrl}api/Eventos/Categorias");
        }

        public async Task<List<EventoModel>> GetEventosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EventoModel>>($"{_baseUrl}api/Eventos");
        }

        public async Task<List<EventoModel>> GetEventosPorCategoriaAsync(int idCategoria)
        {
            // Cambiamos GetFromJsonAsync por GetAsync para poder leer el código de estado antes de que explote
            var response = await _httpClient.GetAsync($"{_baseUrl}api/Eventos/PorCategoria/{idCategoria}");

            if (response.IsSuccessStatusCode)
            {
                // Si hay un 200 OK, deserializamos el JSON normalmente
                return await response.Content.ReadFromJsonAsync<List<EventoModel>>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Si la API devuelve 404 porque la categoría está vacía, devolvemos una lista vacía
                return new List<EventoModel>();
            }
            else
            {
                // Si es un error 500 del servidor u otro fallo grave, dejamos que lance la excepción
                response.EnsureSuccessStatusCode();
                return new List<EventoModel>();
            }
        }
    }
}