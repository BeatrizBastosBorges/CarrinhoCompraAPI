using Microsoft.EntityFrameworkCore;
using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAPI.Infrastructure.Data.Repositories
{
    public class CompraProdutoRepository
    {
        private readonly SqlServerContext _context;

        public CompraProdutoRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<List<CompraProdutoModel>> GetProdutosDaCompra(int compraId)
        {
            return await _context.CompraProdutos
                .Include(cp => cp.Produto)
                .Where(cp => cp.CompraId == compraId)
                .ToListAsync();
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
