using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem
{
    public class AccessLevelDto
    {
        public string name { get; set; }
        public int accessLevelId { get; set; }
        public string description;
        public List<AcrMappingDto> acrs { get; set; }
    }
}
