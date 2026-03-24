using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class HolidayApplyDto
    {
        public int scpId { get; set; }
        public string operation { get; set; }
        public List<HolidayEntryDto> entries { get; set; }
    }

    public class HolidayEntryDto
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int extendDays { get; set; }
        public int typeMask { get; set; }
    }
}
