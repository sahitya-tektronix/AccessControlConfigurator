using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Acr
{
    public class AcrSearchResponse
    {
        public List<AcrDto> data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class Pagination
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public int totalPages { get; set; }
    }
}
