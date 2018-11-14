using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class TarefasController : Controller
    {
        [HttpGet]
        public IActionResult Cadastrar()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("email")))
            {
                return RedirectToAction("Login", "Usuario");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(IFormCollection form)
        {
            TarefasModel tarefas = new TarefasModel();
            tarefas.ID = 1;
            tarefas.Nome = form["nomeTarefa"];
            tarefas.Descricao = form["descricao"];
            tarefas.IdUsuario = int.Parse(form["id"]);
            tarefas.Tipo = form["tipoTarefa"];
            tarefas.DataCriacao = DateTime.Now;

            using (StreamWriter sw = new StreamWriter("tarefas.csv", true)){
                sw.WriteLine($"{tarefas.ID};{tarefas.Nome};{tarefas.Descricao};{tarefas.IdUsuario};{tarefas.Tipo};{tarefas.DataCriacao}");
            }
            ViewBag.Mensagem = "Tarefa Cadastrada";
            return View();
        }
    }
}