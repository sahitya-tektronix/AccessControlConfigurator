using AccessControlSystem.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models
{
    public class DiscoverControllerDto
    {

        public readonly object scpId;

        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string IpAddress { get; set; } = "";
        public string MacAddress { get; set; } = "";

        public bool IsOnline { get; set; }
        public bool IsEnabled { get; set; }

        public string Status { get; set; }

        public int ScpId { get; set; }
      


        public ControllerSyncState SyncState { get; set; }

        public DateTime? LastSyncStartedAt { get; set; }
        public DateTime? LastSyncCompletedAt { get; set; }

        public string Branch { get; set; } = "";
        public string Type { get; set; } = "";

        public bool IsRegistered { get; set; }
        //public int id { get; set; }

        //public string name { get; set; }

        //public int scpId { get; set; }

        //public int? channelId { get; set; }

        //public string macAddress { get; set; }

        //public string serialNumber { get; set; }

        //public int? timeZoneId { get; set; }

        //public int? locationId { get; set; }

        //public string ipAddress { get; set; }

        //public int? port { get; set; }

        //public string subnetMask { get; set; }

        //public string defaultGateway { get; set; }

        //public int connectionType { get; set; }

        //public int connectionMode { get; set; }

        //public int? retryInterval { get; set; }

        //public int deviceId { get; set; }

        //public int deviceVersion { get; set; }

        //public string firmwareVersion { get; set; }

        //public bool isConnected { get; set; }

        //public bool isInitialized { get; set; }

        //public string status { get; set; }

        //public bool isOnline { get; set; }

        //public bool isGeneratedFromIDReport { get; set; }

        //public bool isEnabled { get; set; }

        //public bool internalPort0IsEnabled { get; set; }

        //public int? internalPort0BaudRate { get; set; }

        //public int? internalPort0ProtocolType { get; set; }

        //public bool rs485Port1IsEnabled { get; set; }

        //public int? rs485Port1BaudRate { get; set; }

        //public int? rs485Port1ProtocolType { get; set; }

        //public bool rs485Port2IsEnabled { get; set; }

        //public int? rs485Port2BaudRate { get; set; }

        //public int? rs485Port2ProtocolType { get; set; }

        //public DateTime lastOnlineAt { get; set; }

        //public DateTime? lastOfflineAt { get; set; }

        //public DateTime? lastSyncAttemptAt { get; set; }

        //public DateTime? lastSuccessfulSyncAt { get; set; }

        //public int? activeSyncId { get; set; }

        //public List<object> syncHistory { get; set; }

        //public DateTime createdAt { get; set; }

        //public DateTime updatedAt { get; set; }

        //public bool isDeleted { get; set; }

        //public DateTime? deletedAt { get; set; }

        //public string rawMessage { get; set; }

        //public int syncState { get; set; }

        //public string lastSyncError { get; set; }
    }
}
