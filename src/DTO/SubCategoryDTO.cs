using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  

namespace src.DTO
{
    public class SubCategoryDTO
    {
        public class SubCategoryCreateDto
        {
            public string Name { get ; set;}
        }
        public class SubCategoryReadDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }    
        }

        public class SubCategoryUpdateDto
        {
            public string Name{ get; set; }
        }
    }
}