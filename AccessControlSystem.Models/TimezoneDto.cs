using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class TimezoneDto
    {
    

        public int id { get; set; }
        public int code { get; set; }
        public string name { get; set; }
        public int encScpTimezoneEx { get; set; }
        public int number { get; set; }
        public int mode { get; set; }
        public int actTime { get; set; }
        public int deactTime { get; set; }
        public int intervals { get; set; }
        public int iDays { get; set; }
        public int iStart { get; set; }
        public int iEnd { get; set; }
        public int timeZoneId { get; set; }
    }

    public class TimezoneCreateRequest
    {
        public int number { get; set; }
        public string name { get; set; }
        public int mode { get; set; }
        public int actTime { get; set; }
        public int deactTime { get; set; }
        public int intervals { get; set; }
        public int iDays { get; set; }
        public int iStart { get; set; }
        public int iEnd { get; set; }
    }

    public class TimezoneUpdateRequest : TimezoneCreateRequest
    {
        public int id { get; set; }
    }

    public class TimeZoneDropdownDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }


}
