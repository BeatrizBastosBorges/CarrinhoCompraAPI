using Microsoft.EntityFrameworkCore;
using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaAPI.Infrastructure.Data.Repositories
{
    public class CompraRepository
    {
        private readonly SqlServerContext _context;

        public CompraRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<List<CompraModel>> ListCompras()
        {
            List<CompraModel> list = await _context.Compra.ToListAsync();
            return list;
        }

        public async Task<CompraModel> GetCompra(int compraId)
        {
            CompraModel compra = await _context.Compra.FindAsync(compraId);

            return compra;
        }

        public async Task<CompraModel> CreateCompra(CompraModel compra)
        {
            var resto = Math.Round((compra.ValorTotalCompra - compra.ValorParcela * compra.QtdParcelas), 2);

            compra.ValorParcelaAuxiliar = Math.Round((compra.ValorParcela + resto), 2);

            var result = await _context.Compra.AddAsync(compra);
            await _context.SaveChangesAsync();

            result.State = EntityState.Detached;
            return result.Entity;
        }

        public async Task<bool> DeleteCompra(int compraId)
        {
            var item = await _context.Compra.FindAsync(compraId);

            _context.Compra.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}