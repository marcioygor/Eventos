using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaDeEventos.Data;
using SistemaDeEventos.Models;
using Newtonsoft.Json;

namespace SistemaDeEventos.Controllers;

public class ClienteController : Controller
{
    public readonly Context _context;

    public ClienteController(Context context)
    {
            _context=context;
    }

    public IActionResult Cadastro()
    {
      if (TempData["CadastroError"] != null)
      {
           ModelState.AddModelError(string.Empty, TempData["CadastroError"].ToString());
      }

      return View();

    }

    public IActionResult Login()
    {    
      if (TempData["LoginError"] != null)
      {
           ModelState.AddModelError(string.Empty, TempData["LoginError"].ToString());
      }

      return View();
    }

    public IActionResult LoginCliente(Cliente cliente)
    {    
      
      if (String.IsNullOrEmpty(cliente.Email) || String.IsNullOrEmpty(cliente.Password))
      {
        TempData["LoginError"] = "É nescessário digitar usuário e senha.";
        return RedirectToAction(nameof(Login));
      }

      var usuario=_context.Clientes.FirstOrDefault(x=> x.Email==cliente.Email && x.Password==cliente.Password);

      if (usuario is null){
        TempData["LoginError"] = "Usuário não encontrado. Verifique as credencias de login e senha.";
        return RedirectToAction(nameof(Login));
      }

        return View(cliente);   
      }
          
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrarcliente(Cliente cliente)
    {
        if(ModelState.IsValid){

             if(cliente.Password != cliente.ConfirmPassword){
              TempData["CadastroError"] = "As senhas digitadas não são iguais";
              return RedirectToAction(nameof(Cadastro));
          } 
            var email=_context.Clientes.FirstOrDefault(x=>x.Email==cliente.Email);

            if(email is null){
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
              }

            else
              {
                TempData["CadastroError"] = "Já existe usuário cadastrado com o email " + cliente.Email;
                return RedirectToAction(nameof(Cadastro));
              }

            return View(cliente);        
        }

        else
        {
            var errors = ModelState.Select(x => x.Value.Errors).Where(y=>y.Count>0).ToList();

            TempData["CadastroError"]=JsonConvert.SerializeObject(errors);
           
            return RedirectToAction(nameof(Cadastro));
        }

    }

}