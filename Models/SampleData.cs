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
            if (context.Products.Count() > 0 || context.Categories.Count() > 0)
            {
                return;
            }
            context.Categories.Add(new Category { Name = "E-Book", Description = "E-Books for reading" });
            context.SaveChanges();
            long categoryFirstId = context.Categories.Select(r => r.CategoryId).FirstOrDefault();
            context.Products.AddRange(

                    new Product
                    {
                        Name = "PocketBook 616",
                        Price = 3470,
                        CategoryId = categoryFirstId,
                        Description = "Comfortable device for reading books"                        
                    },
                    new Product
                    {
                        Name = "PocketBook 633",
                        Price = 6056,
                        CategoryId = categoryFirstId,
                        Description = "Comfortable device for reading books"
                    },
                    new Product
                    {
                        Name = "PocketBook 606",
                        Price = 2999,
                        CategoryId = categoryFirstId,
                        Description = "Comfortable device for reading books"
                    }
                    );
            context.SaveChanges();
            
        }
    }
}
