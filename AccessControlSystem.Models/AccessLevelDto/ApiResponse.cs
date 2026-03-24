using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.AccessLevel
{
    public class ApiResponse<T>
    {
        public List<T> data { get; set; }
    }
}
