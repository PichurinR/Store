using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TetstProject.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public  int? StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}