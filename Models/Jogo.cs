using Projeto_Interdisciplinar.Models.Enuns;
using System;

namespace Projeto_Interdisciplinar.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Criador { get; set; }
        public string Empresa { get; set; }
        public DateTime Lançamento { get; set; }       
        public GeneroEnum Genero { get; set; }                      
    }
}