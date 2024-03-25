using Microsoft.EntityFrameworkCore;
using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaAPI.Infrastructure.Data.Repositories
{
    public class CarrinhoRepository
    {
        private readonly SqlServerContext _context;

        public CarrinhoRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<List<CarrinhoModel>> ListProdutosCarrinho()
        {
            List<CarrinhoModel> list = await _context.Carrinho.ToListAsync();
            return list;
        }

        public async Task<CarrinhoModel> GetProdutoCarrinho(int produtoId)
        {
            CarrinhoModel produto = await _context.Carrinho.FindAsync(produtoId);

            return produto;
        }

        public async Task<CarrinhoModel> AddProduto(CarrinhoModel produtoCarrinho)
        {
            var result = await _context.Carrinho.AddAsync(produtoCarrinho);

            await _context.SaveChangesAsync();

            result.State = EntityState.Detached;

            return result.Entity;
        }

        public async Task<int> UpdateProdutoCarrinho(CarrinhoModel itemCarrinho)
        {
            _context.Entry(itemCarrinho).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProdutoCarrinho(int produtoId)
        {
            var item = await _context.Carrinho.FindAsync(produtoId);

            if (item == null)
                return false;

            _context.Carrinho.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}