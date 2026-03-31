using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

namespace AccessControlSystem.Models

{

    public class CardholderDto

    {

        public int cardholderId { get; set; }

        public int? userId { get; set; }

        public string userCode { get; set; }

        public int? accessLevelId { get; set; }

        public string department { get; set; }

        public string middleName { get; set; }

        public string mobile { get; set; }

        public string userName { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public bool isActive { get; set; }

        public long? cardNumber { get; set; }

    }

    public class UpdateCardholderRequest

    {

        public CardholderUpdateDto cardholder { get; set; }

        public CardUpdateDto card { get; set; }

    }

    public class CardholderUpdateDto

    {

        public string firstName { get; set; }

        public string middleName { get; set; }

        public string lastName { get; set; }

        public string mobile { get; set; }

        public string department { get; set; }

        public string email { get; set; }

        public int? accessLevelId { get; set; }

    }

    public class CardUpdateDto

    {

        public int cardNumber { get; set; }

        public string startDateTime { get; set; }

        public string endDateTime { get; set; }

        public List<int> accessLevelIds { get; set; }

    }

}

