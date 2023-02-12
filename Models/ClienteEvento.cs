using System.ComponentModel.DataAnnotations;

namespace SistemaDeEventos.Models
{
    public class ClienteEvento
    {
        [Key]
        public int ClienteId { get; set; }
        public int EventoId { get; set; }
        public string? NomeEvento { get; set; }
    }
}