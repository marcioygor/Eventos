using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaDeEventos.Data;
using SistemaDeEventos.Models;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;

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
//P!ik76%
//tester@gmail.com
            var user=cliente;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(type:ClaimTypes.Email, value:user.Email),
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Password = null;
        return RedirectToAction("Index", "Evento");
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

            return RedirectToAction(nameof(Login));       
        }

        else
        {
            var errors = ModelState.Select(x => x.Value.Errors).Where(y=>y.Count>0).ToList();

            TempData["CadastroError"]=JsonConvert.SerializeObject(errors);
           
            return RedirectToAction(nameof(Cadastro));
        }

    }

}