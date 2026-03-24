using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Cards
{
    public class UpdateCardDto
    {
        public long cardNumber { get; set; }

        public int accessLevelId { get; set; }

        public string startDateTime { get; set; }

        public string endDateTime { get; set; }

        //public int? assignCardholder { get; set; }
        // public string assignCardholder { get; set; }
        public int? assignCardholder { get; set; }

       

    }
}
