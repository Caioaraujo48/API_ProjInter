using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Interdisciplinar.Models
{
    public class Usuario
    {
        public int Id { get; set; } //Atalho para propridade (PROP + TAB)
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [NotMapped] // using System.ComponentModel.DataAnnotations.Schema
        public string PasswordString { get; set; }
        public List<Jogo> JogosFavoritos{ get; set; }//using System.Collections.Generic;
        public string Perfil { get; set; }
        public string Email { get; set; }
    }
}