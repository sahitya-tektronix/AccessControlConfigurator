using System;
using System.Collections.Generic;

namespace AccessControlSystem.Models
{
    public class EventReportFilterRequest
    {
        public List<int> scpIds { get; set; } = new List<int>();
        public List<string> cardNumbers { get; set; } = new List<string>();
        public List<int> cardStatusIds { get; set; } = new List<int>();
        public List<int> replyTypes { get; set; } = new List<int>();
        public List<int> tranTypes { get; set; } = new List<int>();
        public List<int> tranCodes { get; set; } = new List<int>();
        public List<int> tranSourceTypes { get; set; } = new List<int>();
        public string startDate { get; set; }
        public string endDate { get; set; }
        public bool isFilterByCreatedDate { get; set; }
        public List<EventReportExcludedType> excludedEventTypes { get; set; } = new List<EventReportExcludedType>();
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 100;
    }

    public class EventReportExcludedType
    {
        public int replyType { get; set; }
        public int tranType { get; set; }
        public int tranCode { get; set; }
        public int tranSourceType { get; set; }
    }

    public class EventReportResponse
    {
        public List<EventReportItem> data { get; set; } = new List<EventReportItem>();
        public EventReportPagination pagination { get; set; }
    }

    public class EventReportPagination
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public int totalPages { get; set; }
        public bool hasPreviousPage { get; set; }
        public bool hasNextPage { get; set; }
    }

    public class EventReportItem
    {
        public int id { get; set; }
        public DateTime eventDateTime { get; set; }
        public string cardNumber { get; set; }
        public string cardHolder { get; set; }
        public string controllerName { get; set; }
        public string acrName { get; set; }
        public string eventDescription { get; set; }
        public string eventDetails { get; set; }
        public int replyType { get; set; }
        public int tranType { get; set; }
        public int tranCode { get; set; }
        public int tranSourceType { get; set; }
        public int tranTime { get; set; }
        public int scpId { get; set; }
        public int controllerId { get; set; }
        public int acrNumber { get; set; }
        public DateTime createdAt { get; set; }
    }
}
