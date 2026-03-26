using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class AddControllerDto
    {


      public int id { get; set; }
        public string name { get; set; }
        public string macAddress { get; set; }
        public string serialNumber { get; set; }
        public int timeZoneId { get; set; }
        public int locationId { get; set; }
        public string ipAddress { get; set; }
        public int port { get; set; }
        public string subnetMask { get; set; }
        public string defaultGateway { get; set; }

        public bool internalPort0IsEnabled { get; set; }
        public int internalPort0BaudRate { get; set; }
        public int internalPort0ProtocolType { get; set; }

        public bool rs485Port1IsEnabled { get; set; }
        public int rs485Port1BaudRate { get; set; }
        public int rs485Port1ProtocolType { get; set; }

        public bool rs485Port2IsEnabled { get; set; }
        public int rs485Port2BaudRate { get; set; }
        public int rs485Port2ProtocolType { get; set; }
    }
}