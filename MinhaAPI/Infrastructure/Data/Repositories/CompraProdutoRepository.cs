using Microsoft.EntityFrameworkCore;
using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Contexts;
using System;
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
    }
}
