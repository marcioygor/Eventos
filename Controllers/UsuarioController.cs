using System;
using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaDeEventos.Data;
using SistemaDeEventos.Models;

namespace SistemaDeEventos.Controllers;

public class UsuarioController : Controller
{
    public readonly Context _context;

    public UsuarioController(Context context)
    {
            _context=context;
    }

    public IActionResult Cadastro()
    {
        return View();
    }
   
    public IActionResult CadastrarUsuario(Usuario usuario)
    {
        if(ModelState.IsValid){
            
            try
            {
              var email=_context.Usuarios.First(x=>x.Email==usuario.Email);
            }
            catch (Exception e)
            {

             if(!e.Message.Equals("Sequence contains no elements"))
                return BadRequest("Ocorreu um erro inesperado");

             _context.Usuarios.Add(usuario);
             _context.SaveChanges();
             return View(usuario);
            }
                    
           return BadRequest("Já existe usuário cadastrado com este email");
        }

        else
        {
            var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();

            return BadRequest(errors);
        }

    }


}