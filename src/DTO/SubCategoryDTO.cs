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
            // public Guid Id { get ; set;}
            // public List <Product>? products { get; set; }

        }
        public class SubCategoryReadDto
        {
            // public Guid SubCategoryId { get; set; }
            public string Name { get; set; }
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