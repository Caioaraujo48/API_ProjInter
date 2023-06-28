using Projeto_Interdisciplinar.Models.Enuns;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

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
        public byte[]? Foto { get; set; }                     
    }
}