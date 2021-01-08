using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class SampleData
    {
        public static void Initialize(DataContext context)
        {
            if (context.Products.Any())
            {
                return;
            }
            context.Products.AddRange(

                    new Product
                    {
                        Name = "PocketBook 616",
                        Price = 120,
                        Category = "E-Books",
                        Description = "Comfortable device for reading books"                        
                    },
                    new Product
                    {
                        Name = "PocketBook 633",
                        Price = 250,
                        Category = "E-Books",
                        Description = "Comfortable device for reading books"
                    },
                    new Product
                    {
                        Name = "PocketBook 606",
                        Price = 100,
                        Category = "E-Books",
                        Description = "Comfortable device for reading books"
                    }
                    );
            context.SaveChanges();
            
        }
    }
}
