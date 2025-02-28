using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    /// <summary>
    /// Evento disparado quando uma venda é modificada.
    /// </summary>
    public class SaleModifiedEvent
    {
        /// <summary>
        /// O identificador único da venda.
        /// </summary>
        public Guid SaleId { get; }

        /// <summary>
        /// O nome do cliente associado à venda.
        /// </summary>
        public string Customer { get; }

        /// <summary>
        /// A filial onde a venda foi realizada.
        /// </summary>
        public string Branch { get; }

        public SaleModifiedEvent(Guid saleId, string customer, string branch)
        {
            SaleId = saleId;
            Customer = customer;
            Branch = branch;
        }
    }
}
