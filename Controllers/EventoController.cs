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
        _context=context;
    }

    
    public IActionResult Index()
    {

      if(HttpContext.Session.GetInt32("Sessao")!= 1)
           return RedirectToAction("Login", "Cliente");

       return View();
    }


    public IActionResult NovoEvento()
    {
      if(HttpContext.Session.GetInt32("Sessao")!= 1)
           return RedirectToAction("Login", "Cliente");

       return View();
    }

    [HttpPost]
    public IActionResult NovoEvento(Evento evento)
    {
      if(HttpContext.Session.GetInt32("Sessao")!= 1)
           return RedirectToAction("Login", "Cliente");

       return View();
    }

    public IActionResult Logout()
    {
      HttpContext.Session.SetInt32("Sessao", 0);
      return RedirectToAction("Login", "Cliente");
    }

    
}