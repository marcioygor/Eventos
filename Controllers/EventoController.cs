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

    [Authorize]
    public IActionResult Index()
    {
       return View();
    }

    
}