using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class PaginationEntityDTO<TEntity>
    {
        public PaginationEntityDTO()
        {
        }

        public List<TEntity> Entities { get; set; }
        public int TotalEntityCount { get; set; }
    }
}
