using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Acr
{
    public class AcrDto
    {
        public readonly bool isOnline;
        public int controllerId;

        public int id { get; set; }
      
        public int controllerID { get; set; }

        public int sioNumber { get; set; }

        public string name { get; set; }

        public int acrNumber { get; set; }

        public string defaultAcrName { get; set; }

        public int defaultMode { get; set; }

        public int readerNumber { get; set; }

        public int readerType { get; set; }

        public int readerDirection { get; set; }

        public int strikeNumber { get; set; }

        public int doorNumber { get; set; }

        public int rexNumber { get; set; }

        public int acrId { get; set; }
        public int rex0Number { get; set; }

      

 




        // 🔥 Computed property for UI
        public string ReaderName => $"Reader {readerNumber}";
    }
}
