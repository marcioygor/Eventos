using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeEventos.Models
{
    public class EventoCliente
    {

       [Key]
       public int IdEventoCliente { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }

        public string? DescricaoEvento { get; set; }
    }
}