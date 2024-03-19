using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAPI.Domain.Models
{
    public class AbateModel
    {
        public int Id { get; set; }
        public double ValorAbate { get; set; }
        public double NovoValorTotalCompra { get; set; }
        public double NovoValorParcelas { get; set; }
        public double NovoValorParcelaAuxiliar { get; set; }

        public int CompraId { get; set; }
        public CompraModel Compra { get; set; }
    }
}
