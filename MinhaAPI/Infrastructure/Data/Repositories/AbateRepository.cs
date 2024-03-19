using Microsoft.EntityFrameworkCore;
using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAPI.Infrastructure.Data.Repositories
{
    public class AbateRepository
    {
        private readonly SqlServerContext _context;

        public AbateRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<List<AbateModel>> ListComprasAbatidas()
        {
            List<AbateModel> list = await _context.Abate.ToListAsync();
            return list;
        }

        public async Task<AbateModel> GetCompraAbatida(int compraAbatidaId)
        {
            AbateModel compra = await _context.Abate.FindAsync(compraAbatidaId);

            return compra;
        }

        public async Task<AbateModel> CreateCompraAbatida(AbateModel compraAbatida)
        {
            var result = await _context.Abate.AddAsync(compraAbatida);
            await _context.SaveChangesAsync();

            result.State = EntityState.Detached;
            return result.Entity;
        }

        public async Task<int> UpdateCompraAbatida(AbateModel compraAbatida)
        {
            _context.Entry(compraAbatida).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCompraAbatida(int compraAbatidaId)
        {
            var item = await _context.Abate.FindAsync(compraAbatidaId);

            _context.Abate.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
