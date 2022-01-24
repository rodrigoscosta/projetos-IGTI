using BookStore.Data.Contexto;
using BookStore.Filters.AuthorizationFilters;
using BookStore.Filters.ResourceFilters.Caching;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(DynamicAuthorizationFilter))]
    [ApiController]
    public class LivrosController :  ControllerBase
    {
        private LivrosDbContext contexto;
        private readonly SimpleMemoryCache memoryCache;

        public LivrosController(LivrosDbContext contexto, SimpleMemoryCache memoryCache)
        {
            this.contexto = contexto;
            this.memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<ActionResult<Livro>> Post([FromBody]Livro livro)
        {
            contexto.Livros.Add(livro);

            await contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = livro.Id }, livro);
        }

        
        [HttpGet("{id:int}")]
       
        public async Task<ActionResult<Livro>> Get([FromRoute]int id)
        {  

            var livro = await contexto.Livros.FindAsync(id);

            if(livro == null) 
            {
                return NotFound();
            }
          
            return livro;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var livros = await contexto.Livros.ToListAsync();

            return Ok(livros);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Livro>> Put([FromRoute] int id, Livro livro)
        {
            if (id != livro.Id)
            {
                return BadRequest();
            }

            contexto.Entry(livro).State = EntityState.Modified;

            try
            {
                await contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificarSeLivroExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var livro = await contexto.Livros.FindAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            contexto.Livros.Remove(livro);

            await contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool VerificarSeLivroExiste(int id)
        {
            return contexto.Livros.Any(c => c.Id == id);
        }
    }
}
