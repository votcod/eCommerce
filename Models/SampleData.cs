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
            if (context.Products.Count() > 0)
            {
                return;
            }
            context.Products.AddRange(

                    new Product
                    {
                        Name = "PocketBook 616",
                        Price = 120,
                        CategoryId = 1,
                        Description = "Comfortable device for reading books"                        
                    },
                    new Product
                    {
                        Name = "PocketBook 633",
                        Price = 250,
                        CategoryId = 1,
                        Description = "Comfortable device for reading books"
                    },
                    new Product
                    {
                        Name = "PocketBook 606",
                        Price = 100,
                        CategoryId = 1,
                        Description = "Comfortable device for reading books"
                    }
                    );
            context.SaveChanges();
            
        }
    }
}
