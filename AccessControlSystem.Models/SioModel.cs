using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace AccessControlSystem.Models
{
    public class SioModel
    {
        public int Id { get; set; }
        [JsonPropertyName("scpid")]
        public int? ScpId { get; set; }
        public int ControllerID { get; set; }
        public bool IsOnline { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        public int PortNumber { get; set; }
        public int SioNumber { get; set; }
        public int ModelNumber { get; set; }
        public int InterfacePanelAddress { get; set; }
        public string InterfaceType { get; set; }

        public int ComStatus { get; set; }

    }
}
