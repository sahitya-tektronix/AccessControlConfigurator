using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class WiegandDto
    {
        //public int id { get; set; }
        //public int scpId { get; set; }
        //public int formatNumber { get; set; }
        //public string name { get; set; }
        //public int bits { get; set; }
        //public int facilityCode { get; set; }
        //public int flags { get; set; }
        //public int peLen { get; set; }
        //public int peLoc { get; set; }
        //public int poLen { get; set; }
        //public int poLoc { get; set; }
        //public int fcLen { get; set; }
        //public int fcLoc { get; set; }
        //public int chLen { get; set; }
        //public int chLoc { get; set; }
        //public int icLen { get; set; }
        //public int icLoc { get; set; }
        //public DateTime createdAt { get; set; }
        //public DateTime updatedAt { get; set; }

        public short FormatNumber { get; set; }
        public string Name { get; set; }
        public short Bits { get; set; }
        public short FacilityCode { get; set; }

        public short Flags { get; set; }
        public short PeLen { get; set; }
        public short PeLoc { get; set; }
        public short PoLen { get; set; }
        public short PoLoc { get; set; }
        public short FcLen { get; set; }
        public short FcLoc { get; set; }
        public short ChLen { get; set; }
        public short ChLoc { get; set; }
        public short IcLen { get; set; }
        public short IcLoc { get; set; }
    }
}
