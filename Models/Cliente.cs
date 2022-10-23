using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeEventos.Models;

public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Informe o seu Nome.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Informe o seu email.")]
    [StringLength(50)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
        ErrorMessage = "O email não possui um formato correto")]
    public string Email { get; set; }

    [Display(Name = "Informe a Data de Nascimento")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "Informe o seu telefone")]
    [StringLength(25)]
    [DataType(DataType.PhoneNumber)]
    public string Telefone { get; set; }

    [RegularExpression("^(?=.*[A-Z])(?=.*[!#@$%&])(?=.*[0-9])(?=.*[a-z]).{6,15}$",
    ErrorMessage =  "A senha deve ter tamanho mínimo 6 e no máximo 15 caracteres." +
                    "ter letras e numero e caractere especial(!#@$%&)."+
                    "no mínimo uma letra maiúscula e minúscula." +
                    "no mínimo um numero." +
                    "no mínimo caractere especial(!#@$%&)")]
    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public List<Evento> Eventos;

}