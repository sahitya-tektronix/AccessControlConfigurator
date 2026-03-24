using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class SioBulkConfigDto
    {
        public bool isInternalSIOEnabled { get; set; }
        public List<SioItemDto> sios { get; set; }
    }

    public class SioItemDto
    {
        public readonly int bus;

        public string name { get; set; }
        public int portNumber { get; set; }
        public int sioNumber { get; set; }
        public int modelNumber { get; set; }
        public int interfacePanelAddress { get; set; }
    }
}
    

