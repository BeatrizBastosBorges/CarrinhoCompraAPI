using Microsoft.EntityFrameworkCore;
using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaAPI.Infrastructure.Data.Repositories
{
    public class ProdutoRepository
    {
        private readonly SqlServerContext _context;

        public ProdutoRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoModel>> ListProdutos()
        {
            List<ProdutoModel> list = await _context.Produto.ToListAsync();
            return list;
        }

        public async Task<ProdutoModel> GetProduto(int produtoId)
        {
            ProdutoModel produto = await _context.Produto.FindAsync(produtoId);

            return produto;
        }

        public async Task<ProdutoModel> CreateProduto(ProdutoModel produto)
        {
            var result = await _context.Produto.AddAsync(produto);

            await _context.SaveChangesAsync();

            result.State = EntityState.Detached;

            return result.Entity;
        }

        public async Task<int> UpdateProduto(ProdutoModel produto)
        {
            _context.Entry(produto).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProduto(int produtoId)
        {
            var item = await _context.Produto.FindAsync(produtoId);

            _context.Produto.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}