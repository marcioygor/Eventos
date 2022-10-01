using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaDeEventos.Data;

namespace SistemaDeEventos.Controllers;

public class UsuarioController : Controller
{

    public IActionResult Cadastro()
    {
        return View();
    }

}