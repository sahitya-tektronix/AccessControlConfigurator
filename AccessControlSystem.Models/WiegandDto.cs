using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class WiegandDto
    {
        public int id { get; set; }
        public int scpId { get; set; }
        public int formatNumber { get; set; }
        public string name { get; set; }
        public int bits { get; set; }
        public int facilityCode { get; set; }
        public int flags { get; set; }
        public int peLen { get; set; }
        public int peLoc { get; set; }
        public int poLen { get; set; }
        public int poLoc { get; set; }
        public int fcLen { get; set; }
        public int fcLoc { get; set; }
        public int chLen { get; set; }
        public int chLoc { get; set; }
        public int icLen { get; set; }
        public int icLoc { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
