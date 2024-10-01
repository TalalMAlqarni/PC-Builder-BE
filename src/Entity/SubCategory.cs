using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace src.Entity
{
    public class SubCategory
    {
        // Category forigen key 
        public Guid Id { get; set; }
        // SubCategory primary id 
        public Guid SubCategoryId  { get; set; }
        public string? Name { get; set; }
        public List<Product>? Products { get; set; }
        public Category? Category { get; set; }  // Navigation property


    }
}    
