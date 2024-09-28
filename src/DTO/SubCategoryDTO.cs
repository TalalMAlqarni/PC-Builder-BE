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
            public Guid subId { get; set; }
            public string Name { get; set; }    
        }

        public class SubCategoryUpdateDto
        {
            public Guid subId { get; set; }
            public string Name{ get; set; }

        }
        public class SubCategoryDeleteDto
        {
            public string Name{ get; set; }
            public Guid subId { get; set; }
        }
    }
}