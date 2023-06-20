using Microsoft.AspNetCore.Mvc;
using Projeto_Interdisciplinar.Data;
using Projeto_Interdisciplinar.Models; 
using Microsoft.EntityFrameworkCore;

namespace Projeto_Interdisciplinar.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : ControllerBase
    {
         private readonly DataContext _context; //Declaração do atributo

        public UsuariosController(DataContext context)
        {
            //Inicialização do atributo a partir de um parâmetro          
            _context = context;
        }

         [HttpGet("{id}")] //Buscar pelo id
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Usuario p = await _context.Usuarios.FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(p);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
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

        [HttpPost]
        public async Task<IActionResult> Add(Usuario novoUsuario)
        {
            try
            {
                await _context.Usuarios.AddAsync(novoUsuario);
                await _context.SaveChangesAsync();

                return Ok(novoUsuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Usuario novoUsuario)
        {
            try
            {               
                await _context.Usuarios.Update(novoUsuario);
                await _context.SaveChangesAsync();

                return Ok(novoUsuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}