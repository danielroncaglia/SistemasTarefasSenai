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
            TarefasModel tarefaModel = new TarefasModel();
            if(System.IO.File.Exists("tarefas.csv")){
                String[] linhas = System.IO.File.ReadAllLines("tarefas.csv");
                tarefaModel.ID = linhas.Length +1;
            }
            else{
                tarefaModel.ID = 1;
            }
            tarefaModel.Nome = form["tarefa"];
            tarefaModel.Descricao = form["descricao"];
            tarefaModel.Tipo = form["tipo"];
            tarefaModel.IdUsuario = HttpContext.Session.GetInt32("idUsuario").Value;
            tarefaModel.DataCriacao = DateTime.Now;

            using (StreamWriter sw = new StreamWriter("tarefas.csv", true)){
                sw.WriteLine($"{tarefaModel.ID};{tarefaModel.Nome};{tarefaModel.Descricao};{tarefaModel.Tipo};{tarefaModel.IdUsuario};{tarefaModel.DataCriacao}");
            }
            ViewBag.Mensagem = "Tarefa Cadastrada";
            return View();
        }
    }
}