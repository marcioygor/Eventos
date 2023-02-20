using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaDeEventos.Data;
using SistemaDeEventos.Models;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Authorization;

namespace SistemaDeEventos.Controllers;

public class EventoController : Controller
{
    public readonly Context _context;

    public EventoController(Context context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetInt32("Sessao") != 1)
            return RedirectToAction("Login", "Cliente");

        var eventos = _context.Eventos.ToList();

        return View(eventos);
    }


    public IActionResult NovoEvento()
    {
        if (HttpContext.Session.GetInt32("Sessao") != 1)
            return RedirectToAction("Login", "Cliente");

        if (TempData["EventoError"] != null)
        {
            ModelState.AddModelError(string.Empty, TempData["EventoError"].ToString());
        }

        return View();
    }

    [HttpPost]
    public IActionResult NovoEvento(Evento evento)
    {
        if (HttpContext.Session.GetInt32("Sessao") != 1)
            return RedirectToAction("Login", "Cliente");

        if (evento.CapacidadeMaximaPessoas < 5)
        {
            TempData["EventoError"] = "É nescessário ter no mínimo 5 participantes.";
            return RedirectToAction(nameof(NovoEvento));
        }

        evento.clienteId = Settings.ClienteId;
        evento.NumeroDePessoasParticipantes = 0;
        _context.Eventos.Add(evento);
        _context.SaveChanges();

        return RedirectToAction("Index", "Evento");
    }

    public IActionResult ParticiparEvento(string EventoId)
    {
        if (HttpContext.Session.GetInt32("Sessao") != 1)
            return RedirectToAction("Login", "Cliente");
      
        int IdEventoNum;
        bool conversaoBemSucedida = int.TryParse(EventoId, out IdEventoNum);
        EventoCliente eventoCliente=new EventoCliente();
        
        if(!conversaoBemSucedida) return Content("Ocorreu um erro no servidor.");
 
        var evento= _context.Eventos.FirstOrDefault(x=> x.EventoId==IdEventoNum);

        if(evento is null) return NotFound("Evento não encontrado");

        if (evento.NumeroDePessoasParticipantes< evento.CapacidadeMaximaPessoas)
        {
            evento.NumeroDePessoasParticipantes+=1;
        }

        else
        {
            return BadRequest("Evento lotado.");
        }

         eventoCliente.ClienteId=Settings.ClienteId;
         eventoCliente.EventoId=evento.EventoId;
         eventoCliente.DescricaoEvento=evento.DescricaoEvento;

        _context.EventoClientes.Add(eventoCliente);
        _context.SaveChanges();

         return Ok(EventoId);

        }

     public IActionResult Logout()
    {
        HttpContext.Session.SetInt32("Sessao", 0);
        return RedirectToAction("Login", "Cliente");
    }

    }

    

    

  
    


