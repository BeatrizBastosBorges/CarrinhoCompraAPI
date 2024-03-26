using CarrinhoCompraAPI.Domain.Models;
using CarrinhoCompraAPI.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarrinhoCompraAPI.Infrastructure.Data.Repositories
{
    public class CompraProdutoRepository
    {
        private readonly SqlServerContext _context;

        public CompraProdutoRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<List<CompraProdutoModel>> ListProdutosDaCompra(int compraId)
        {
            List<CompraProdutoModel> list = await _context.CompraProdutos
                .Include(cp => cp.Produto)
                .Where(cp => cp.CompraId == compraId)
                .ToListAsync();

            return list;
        }

        public async Task<bool> ProdutoVinculadoACompra(int produtoId)
        {
            return await _context.CompraProdutos.AnyAsync(cp => cp.ProdutoId == produtoId);
        }

        public async Task<CompraProdutoModel> GetCompraProduto(int compraId, int produtoId)
        {
            return await _context.CompraProdutos
                .Include(cp => cp.Compra)
                .Include(cp => cp.Produto)
                .FirstOrDefaultAsync(cp => cp.CompraId == compraId && cp.ProdutoId == produtoId);
        }

        public async Task AddCompraProduto(CompraProdutoModel compraProduto)
        {
            _context.CompraProdutos.Add(compraProduto);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> DeleteProdutosDaCompra(int compraId)
        {
            var compraProdutos = await _context.CompraProdutos.Where(cp => cp.CompraId == compraId).ToListAsync();

            if (compraProdutos == null || !compraProdutos.Any())
                return false;

            _context.CompraProdutos.RemoveRange(compraProdutos);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
