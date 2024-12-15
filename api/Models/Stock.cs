using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;
        [Column (TypeName = "decimal(18,2)")] // Decimal(18,2) is a data type in SQL Server that represents a decimal value with a precision of 18 digits and a scale of 2 digits.

        public decimal PurchasePrice { get; set; }
        [Column (TypeName = "decimal(18,2)")]

        public decimal LastDividend { get; set; } 

        public string Industry { get; set; } = string.Empty; 

        public long MarketCap { get; set; }


        public List<Comment> Comments { get; set; } = new List<Comment>(); // Navigation property for comments
    }
}