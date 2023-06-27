using Microsoft.AspNetCore.Mvc;
using Projeto_Interdisciplinar.Data;
using Projeto_Interdisciplinar.Models; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Projeto_Interdisciplinar.Controllers;

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
                Jogo p = await _context.Jogos.FirstOrDefaultAsync(pBusca => pBusca.Id == id);

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
                List<Jogo> lista = await _context.Jogos.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Jogo novoJogo)
        {
            try
            {
                await _context.Jogos.AddAsync(novoJogo);
                await _context.SaveChangesAsync();

                return Ok(novoJogo);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Jogo novoJogo)
        {
            try
            {
                _context.Jogos.Update(novoJogo);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(novoJogo);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                 Jogo pRemover = await _context.Jogos.FirstOrDefaultAsync(p => p.Id == id);

                _context.Jogos.Remove(pRemover);
                int linhaAfetadas = await _context.SaveChangesAsync();
                return Ok(linhaAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);                
            }
        }
    }