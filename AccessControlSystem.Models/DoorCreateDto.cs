using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class DoorCreateDto
    {
        public int controllerId { get; set; }
        public int sioAddress { get; set; }
        public int portNumber { get; set; }
        public string name { get; set; }
    }
}
