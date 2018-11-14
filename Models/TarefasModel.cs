using System;

namespace Sistema.Models
{
    public class TarefasModel
    {

        //declarando variáveis das tarefas
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int IdUsuario { get; set; }
        public string Tipo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}