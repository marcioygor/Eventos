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

    public IActionResult ParticiparEvento([FromRoute] string EventoId)
    {

        if (HttpContext.Session.GetInt32("Sessao") != 1)
            return RedirectToAction("Login", "Cliente");

         return Ok(EventoId);

        }

     public IActionResult Logout()
    {
        HttpContext.Session.SetInt32("Sessao", 0);
        return RedirectToAction("Login", "Cliente");
    }

    }

    

    

  
    


