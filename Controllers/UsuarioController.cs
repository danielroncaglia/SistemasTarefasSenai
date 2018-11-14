using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        //cadastro de usuario
        public ActionResult Cadastrar(IFormCollection form)
        {
            UsuarioModel usuarioModel = new UsuarioModel();

            if(System.IO.File.Exists("usuarios.csv")){
                String[] linhas = System.IO.File.ReadAllLines("usuarios.csv");
                usuarioModel.ID = linhas.Length +1;
            }
            else{
                usuarioModel.ID = 1;
            }
            usuarioModel.Nome = form["nomeUsuario"];
            usuarioModel.Email = form["email"];
            usuarioModel.Senha = form["senha"];
            usuarioModel.Tipo = form["tipoUsuario"];
            usuarioModel.DataCriacao = DateTime.Now;

            using (StreamWriter sw = new StreamWriter("usuarios.csv", true))
            {
                sw.WriteLine($"{usuarioModel.ID};{usuarioModel.Nome};{usuarioModel.Email};{usuarioModel.Senha};{usuarioModel.Tipo};{usuarioModel.DataCriacao}");
            }

            ViewBag.Mensagem = "Usuário Cadastrado";
            return View();
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            UsuarioModel usuario = new UsuarioModel();
            usuario.Email = form["email"];
            usuario.Senha = form["senha"];

            using (StreamReader sr = new StreamReader("usuarios.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string[] dados = sr.ReadLine().Split(";");

                    if (dados[2] == usuario.Email && dados[3] == usuario.Senha)
                    {
                        HttpContext.Session.SetString("email", usuario.Email);
                        HttpContext.Session.SetInt32("idUsuario", usuario.ID);
                        return RedirectToAction("Cadastrar", "Tarefas");
                    }
                }
            }
            ViewBag.Mensagem = "Usuário inválido";

            return View();
        }
    }
}