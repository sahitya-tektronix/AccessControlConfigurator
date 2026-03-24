using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    namespace AccessControlSystem.Models
    {
        public class SioBulkDto
        {
            public int Bus { get; set; }        // 1 = RS485-1, 2 = RS485-2
            public int Address { get; set; }    // 0 - 31
            public string Type { get; set; }    // X100, X110, X200

            public int hardwareId { get; set; }
            public int sioNumber { get; set; }
            public string type { get; set; }
            public string portNumber { get; set; }
            public int address { get; set; }
        
    }
    }
}
