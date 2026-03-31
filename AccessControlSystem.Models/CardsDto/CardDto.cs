using System;

namespace AccessControlSystem.Models.Cards
{
    public class CardDto
    {
        public int id { get; set; }

        public int enCcAdbCard { get; set; }

        public int scpId { get; set; }

        public long cardNumber { get; set; }   // FIXED

        public int? accessLevelId { get; set; }
        public string accessLevelName { get; set; }

        public int issueCode { get; set; }

        public int flags { get; set; }

        public string pin_MAX_PIN_EXTD { get; set; }

        public string alvl_MAX_ALVL_EXTD { get; set; }

        public int apb_Loc { get; set; }

        public int use_Count { get; set; }

        public string alvls { get; set; }

        public int actTime { get; set; }

        public int dactTime { get; set; }

        public int vacDate { get; set; }

        public int vacDays { get; set; }

        public int tmpDate { get; set; }

        public int tmpDays { get; set; }

        public string user_Level_MAX_ULVL { get; set; }

        public string alvl_Prec_MAX_ACR_PER_SCP { get; set; }

        public string acrNumbers { get; set; }

        public string acrScpIds { get; set; }

        public int status { get; set; }

        public int isDeleted { get; set; }

        public string raw_Command { get; set; }

        public DateTime lastModified { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public string startDateTime { get; set; }

        public string endDateTime { get; set; }

        public int? assignCardholder { get; set; }
    }

   
  
    }
