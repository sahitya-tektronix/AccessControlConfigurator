using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.AccessLevelDto.AccessLevelDto
{
    public class AccessLevelCreateDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<AcrTimeZoneDto> acrs { get; set; }
    }

    public class AcrTimeZoneDto
    {
        public int acrId { get; set; }
        public int timeZoneId { get; set; }
    }
    //public class AccessLevelCreateDto
    //{
    //    public string Name { get; set; }

    //    public string Description { get; set; }

    //    public List<AccessLevelAcrDto> Acrs { get; set; }
    //}

}
