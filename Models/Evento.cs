using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeEventos.Models;

public class Evento
{
    [Key]
    public int EventoId { get; set; }

    [Required(ErrorMessage = "Informe a Capacidade Máxima de Pessoas")]
    public int CapacidadeMaximaPessoas { get; set; }

    [Required(ErrorMessage = "Informe a Descrição do Evento")]
    [MinLength(20)]
    [MaxLength(200)]
    public string DescricaoEvento { get; set; }

    [Required(ErrorMessage = "Informe a UF do Evento")]
    [MinLength(2)]
    [MaxLength(2)]
    public string UF { get; set; }

    [Required(ErrorMessage = "Informe a Cidade do Evento")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "Informe o Bairro do Evento")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "Informe a Rua do Evento")]
    [MinLength(2)]
    [MaxLength(30)]

    public string Rua { get; set; }

    [Required(ErrorMessage = "Informe o CEP do Evento")]
    [MinLength(2)]
    [MaxLength(30)]

    public string CEP { get; set; }

    public Cliente cliente { get; set; }

    public int clienteId { get; set; }
    public int NumeroDePessoasParticipantes { get; set; }

}