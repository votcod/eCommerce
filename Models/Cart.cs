using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection
                .FirstOrDefault(p => p.Product.ProductId == product.ProductId);               

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    ProductId = product.ProductId,
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product) =>
            lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);

        public decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Product.Price * e.Quantity);

        public void Clear() => lineCollection.Clear();

        public IEnumerable<CartLine> Lines => lineCollection;
    }
}
