using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class ShoppingCart 
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product? Product { get; set; }
        [Range(1, 1000, ErrorMessage = "Please Enter A Value Between 1 and 1000")]
        public int Count { get; set; }
        public string ?RegisteredUserId { get; set; }
        [ForeignKey("RegisteredUserId")]
        [ValidateNever]
        public RegisteredUser? User { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
