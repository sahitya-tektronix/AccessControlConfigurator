using System;
using System;
using AccessControlSystem.Models.Enum;
namespace AccessControlSystem.Models
{
    public class ControllerDto
    {
        public readonly object scpId;
        public int timeZoneId;

        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string IpAddress { get; set; } = "";
        public string MacAddress { get; set; } = "";

        public bool IsOnline { get; set; }
        public bool IsEnabled { get; set; }

        public int Status { get; set; }

        public int ScpId { get; set; }

        public ControllerSyncState SyncState { get; set; }

        public DateTime? LastSyncStartedAt { get; set; }
        public DateTime? LastSyncCompletedAt { get; set; }

        public string Branch { get; set; } = "";
        public string Type { get; set; } = "";

        public bool IsRegistered { get; set; }
        public int TimeZoneId { get; set; }
        //public string TimeZoneId { get; set; } = string.Empty;
    }
}