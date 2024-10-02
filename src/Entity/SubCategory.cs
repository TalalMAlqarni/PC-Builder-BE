using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace src.Entity
{
    public class SubCategory
    {
        public Guid SubCategoryId  { get; set; } // SubCategory primary id 
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
        public Guid CategoryId { get; set; } // Category forigen key 
        public Category Category { get; set; }  // Navigation property
    }
}    
