using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperMercado
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly List<Produto> _produtos = new List<Produto>();

        [HttpPost]
        public ActionResult CadastrarProduto([FromBody] Produto produto)
        {
            produto.Id = _produtos.Count+1;
            _produtos.Add(produto);

            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarProduto(int id, [FromBody] Produto produto)
        {
            var produtoExistente = _produtos.FirstOrDefault(p => p.Id == id);
            if (produtoExistente == null)
            {
                return NotFound();
            }

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.Informacoes = produto.Informacoes;

            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> GetProduto(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            return produto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            return Ok(_produtos);
        }
    }
}
