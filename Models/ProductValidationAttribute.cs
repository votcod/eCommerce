using eCommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class ProductValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            ProductEditViewModel product = value as ProductEditViewModel;
            string s = product.Description.Split()[0];
            string p = product.Name;

            char h = product.Name[0];
            char b = product.Name.ToUpper()[0];

            if (!(product.Name[0] == product.Name.ToUpper()[0] 
                && product.Description.Split()[0] == product.Name.Split()[0]))
            {                
                this.ErrorMessage = "First letter of Name must be in capital case and first word of description must be equal to name";
                return false;
            }
            return true;
        }
    }
}
