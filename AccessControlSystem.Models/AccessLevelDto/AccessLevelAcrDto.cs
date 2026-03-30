using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem
{
    public class AccessLevelAcrDto
    {
        public object timeZoneId;

        public int accessLevelId { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public List<AcrMappingDto> acrs { get; set; }
        public object? AcrId { get; set; }
        public object? TimeZoneId { get; set; }
    }

    public class AcrMappingDto
    {
        public int acrId { get; set; }
        public string acrName { get; set; }
        public int? timeZoneId { get; set; }
    }
}
