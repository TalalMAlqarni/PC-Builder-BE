using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Entity;
using static src.DTO.ProductDTO;

namespace src.DTO
{
    public class SubCategoryDTO
    {
        public class SubCategoryCreateDto
        {
            // public Guid Id;

            public string Name { get ; set;}
            public Guid CategoryId { get ; set;}
            // public List <Product>? products { get; set; }

        }
  
        public class SubCategoryReadDto
        {
            public Guid SubCategoryId { get; set; }
            public string Name { get; set; }

            public Guid CategoryId{ get; set; }
            // public Category Category { get; set; }
            public string CategoryName { get; set; } // Add CategoryName property
// if i would like to see all category details
            // public List<GetProductDto?> Products { get; set; }
        }

        public class SubCategoryUpdateDto
        {
            // public Guid SubCategoryId { get; set; }
            public string Name{ get; set; }
            public Guid Id { get ; set;}


        }
        public class SubCategoryDeleteDto
        {
            public Guid Id { get ; set;}

            public Guid SubCategoryId { get; set; }
            public string Name{ get; set; }
        }
    }
}