using Microsoft.EntityFrameworkCore;
using Projeto_Interdisciplinar.Models;
using Projeto_Interdisciplinar.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto_Interdisciplinar.Utils;

namespace Projeto_Interdisciplinar.Data 
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogo>().HasData
            (
                new Jogo() {Id = 1, Nome = "Diablo 3", Criador = "Leonard Boyarsky", Empresa = "Blizzard", Lançamento = Convert.ToDateTime("2012-03-15"), Genero = GeneroEnum.RPG },
                new Jogo() {Id = 2, Nome = "Shadow of the Colossus", Criador = "Fumito Ueda", Empresa = "Team Ico", Lançamento = Convert.ToDateTime("2005-10-18"), Genero = GeneroEnum.AçãoAventura },
                new Jogo() {Id = 3, Nome = "Assassin's Creed", Criador = "Patrice Désilets", Empresa = "Ubisoft", Lançamento = Convert.ToDateTime("2007-11-13"), Genero = GeneroEnum.Stealth },
                new Jogo() {Id = 4, Nome = "Alien: Isolation", Criador = "Creative Assembly", Empresa = "Creative Assembly", Lançamento = Convert.ToDateTime("2007-11-13"), Genero = GeneroEnum.Suspense },
                new Jogo() {Id = 5, Nome = "Call of Duty: Modern Warfare", Criador = "Infinity Ward", Empresa = "Infinity Ward", Lançamento = Convert.ToDateTime("2007-10-25"), Genero = GeneroEnum.FPS },
                new Jogo() {Id = 6, Nome = "Dark Souls Remastered", Criador = "Hidetaka Miyazaki", Empresa = "FromSoftware", Lançamento = Convert.ToDateTime("2018-03-24"), Genero = GeneroEnum.ActionRpg}
            );  

            Usuario user = new Usuario();
            Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[]salt);
            user.Id = 1;
            user.Username = "UsuarioAdmin";
            user.PasswordString = string.Empty;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.Perfil = "Admin";
            user.Email = "seuEmail@gmail.com";

            modelBuilder.Entity<Usuario>().HasData(user);

            modelBuilder.Entity<Usuario>().Property(u => u.Perfil).HasDefaultValue("Usuario");
        }
    }
}