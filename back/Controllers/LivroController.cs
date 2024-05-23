using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly Config _context;

        public LivroController(Config context)
        {
            _context = context;
        }

        // GET: api/Livro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
        {
            return await _context.Livros.Include(l => l.Categorias).ToListAsync();
        }

        // GET: api/Livro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivro(int id)
        {
            var livro = await _context.Livros.Include(l => l.Categorias).FirstOrDefaultAsync(l => l.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }

        // PUT: api/Livro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, LivroCreateDto livroDto)
        {
            if (!LivroExists(id))
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.Categorias)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            // Atualiza as propriedades do livro
            livro.Nome = livroDto.Nome;
            livro.Preco = livroDto.Preco;
            livro.FaixaEtaria = livroDto.FaixaEtaria;

            // Atualiza as categorias
            var existingCategories = livro.Categorias.ToList();
            var newCategories = livroDto.Categorias.Select(c => c.Tipo).ToList();

            // Remover categorias que não estão mais presentes
            foreach (var existingCategory in existingCategories)
            {
                if (!newCategories.Contains(existingCategory.Tipo))
                {
                    _context.Categorias.Remove(existingCategory);
                }
            }

            // Adicionar novas categorias ou atualizar existentes
            foreach (var newCategoryTipo in newCategories)
            {
                var existingCategory = existingCategories.FirstOrDefault(c => c.Tipo == newCategoryTipo);
                if (existingCategory == null)
                {
                    livro.Categorias.Add(new Categoria { Tipo = newCategoryTipo, LivroId = livro.Id });
                }
                else
                {
                    // Se necessário, atualize outras propriedades da categoria aqui
                    existingCategory.Tipo = newCategoryTipo;
                    _context.Entry(existingCategory).State = EntityState.Modified;
                }
            }

            _context.Entry(livro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
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

        // POST: api/Livro
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(LivroCreateDto livro)
        {
            
            var novoLivro = new Livro
            {
                Nome = livro.Nome,
                Preco = livro.Preco,
                FaixaEtaria = livro.FaixaEtaria,
            };

            var Categorias = livro.Categorias.Select(c => new Categoria { Tipo = c.Tipo, Livro = novoLivro }).ToList();

            novoLivro.Categorias = Categorias;

            _context.Livros.Add(novoLivro);
            await _context.SaveChangesAsync();

            return Ok(await _context.Livros.Include(l => l.Categorias).ToListAsync());
        }

        // DELETE: api/Livro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
