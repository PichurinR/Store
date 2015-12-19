using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TetstProject.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        public string Name { get; set; }

       public virtual ICollection<Product> Products { get; set; }

        public Store()
        {
            Products = new List<Product>();
        }
    }
}
