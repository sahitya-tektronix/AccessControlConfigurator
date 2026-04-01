using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessControlSystem.Models;

namespace AccessControlSystem.Models.Cards
{
    public class CreateCardDto
    {
        public long cardNumber { get; set; }

        public int accessLevelId { get; set; }

        public DateTime? startDateTime { get; set; }

        public DateTime? endDateTime { get; set; }

        public int? assignCardholder { get; set; }
       // public ControllerSyncState SyncState { get; set; }
    }
}
