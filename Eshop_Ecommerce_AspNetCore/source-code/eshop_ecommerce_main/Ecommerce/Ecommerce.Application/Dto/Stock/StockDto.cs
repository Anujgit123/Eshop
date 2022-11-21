using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using System;

namespace Ecommerce.Application.Dto
{
    public class StockDto
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public Variant Variant { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public StockInputType StockInputType { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
