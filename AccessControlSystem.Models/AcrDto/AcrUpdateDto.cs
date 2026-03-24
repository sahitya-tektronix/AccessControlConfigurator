using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Acr
{
    public class AcrUpdateDto
    {
        public string Name { get; set; }
        public int AcrNumber { get; set; }
        public int DefaultMode { get; set; }
        public int ReaderNumber { get; set; }
        public int ReaderType { get; set; }
        public int StrikeNumber { get; set; }
        public int DoorNumber { get; set; }
        public int RexNumber { get; set; }
    }
}
