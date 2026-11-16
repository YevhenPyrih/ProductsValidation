using ProductsValidation.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsValidation.Models
{
    public class Product
    {
        public enum Category { Toy, Technique, Clothes, Transport}

        public int Id { get; set; }


        public Category Type { get; set; }

        [Required]
        public string Name { get; set; }

        [MinLength(2)]
        [StartsWithName]
        public string Description { get; set; }

        [Range(0.0, 100000.0)]
        public decimal Price { get; set; }
    }
}
