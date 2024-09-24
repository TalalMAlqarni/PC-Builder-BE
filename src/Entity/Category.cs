using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace src.Entity
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

        public class SubCategory
    {
        // Category forigen key 
        public Guid Id { get; set; }
        // SubCategory primary id 
        public Guid subId { get; set; }
        public string Name { get; set; }
    }

}
