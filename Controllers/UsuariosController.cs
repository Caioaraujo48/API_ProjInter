using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Projeto_Interdisciplinar.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_Interdisciplinar.Models;
using System.Collections.Generic;
using System.Linq;
using Projeto_Interdisciplinar.Utils;



[ApiController]
[Route("[Controller]")]
public class UsuariosController : ControllerBase
{
    private readonly DataContext _context;
    public UsuariosController(DataContext context)
    {
        _context = context;
    }

    private async Task<bool> UsuarioExistente(string username)
    {
        if (await _context.Usuarios.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
        {
            return true;
        }
        return false;
    }

    [HttpPost("Registrar")]

    public async Task<IActionResult> RegistrarUsuario(Usuario user)
    {
        try
        {
            if (await UsuarioExistente(user.Username))
                throw new System.Exception("Nome de usuario já existe");

            Criptografia.CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[] salt);
            user.PasswordString = string.Empty;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user.Id);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("AlterarSenha")]
    public async Task<IActionResult> AlterarSenhaUsuario (Usuario credenciais)
    {
        try
        {
            Usuario usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));

            if (usuario == null)
            {
                throw new System.Exception("Usuário não encontrado.");
            }
            Criptografia.CriarPasswordHash(credenciais.PasswordString, out byte[] hash, out byte [] salt);
            usuario.PasswordHash = hash;
            usuario.PasswordSalt = salt;

            _context.Usuarios.Update(usuario);
            int linhasAfetadas = await _context.SaveChangesAsync();
            return Ok(linhasAfetadas);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetUsuario()
    {
        try
        {
            List<Usuario> lista = await _context.Usuarios.ToListAsync();
            return Ok(lista);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        try
        {
            Jogo p = await _context.Jogos
                .Include(no => no.Nome)
                .Include(ge => ge.Genero)
                .Include(em => em.Empresa)
                .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

            return Ok(p);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{usuarioId}")] 
    public async Task<IActionResult> GetUsuario(int usuarioId)
    { 
        try            
        { 
            //List exigirá o using System.Collections.Generic
            Usuario usuario = await _context.Usuarios //Busca o usuário no banco através do Id                   
                .FirstOrDefaultAsync(x => x.Id == usuarioId); 	
            return Ok(usuario);             
            } 
        catch (System.Exception ex)             
        { 
            return BadRequest(ex.Message);             
        }         
    } 

    [HttpGet("GetByLogin/{login}")] 
    public async Task<IActionResult> GetUsuario(string login)         
    { 
        try            
        { 
            //List exigirá o using System.Collections.Generic
            Usuario usuario = await _context.Usuarios //Busca o usuário no banco através do login                   
                .FirstOrDefaultAsync(x => x.Username.ToLower() == login.ToLower()); 
            return Ok(usuario);             
        } 
        catch (System.Exception ex)             
        { 
            return BadRequest(ex.Message);             
        }         
    } 

    //Método para alteração do e-mail        
    [HttpPut("AtualizarEmail")] 
    public async Task<IActionResult> AtualizarEmail(Usuario u)         
    { 
        try            
        { 
            Usuario usuario = await _context.Usuarios //Busca o usuário no banco através do Id                   
                .FirstOrDefaultAsync(x => x.Id == u.Id); 
            usuario.Email = u.Email;                 
            
            var attach = _context.Attach(usuario); 
            attach.Property(x => x.Id).IsModified = false; 
            attach.Property(x => x.Email).IsModified = true;                 

            int linhasAfetadas = await _context.SaveChangesAsync(); //Confirma a alteração no banco
            return Ok(linhasAfetadas); //Retorna as linhas afetadas (Geralmente sempre 1 linha msm)            
        } 
        catch (System.Exception ex)             
        { 
            return BadRequest(ex.Message);             
        }         
    }
}