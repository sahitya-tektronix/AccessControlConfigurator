using AccessControlSystem.Models;

namespace AccessControlConfigurator.Cardholders
{
    internal class CardholderUpdate : CardholderUpdateDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        public string department { get; set; }
        public string email { get; set; }
        public int accessLevelId { get; set; }
    }
}