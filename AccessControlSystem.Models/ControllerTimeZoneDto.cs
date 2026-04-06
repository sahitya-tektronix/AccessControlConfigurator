namespace AccessControlSystem.Models
{
    public class ControllerTimeZoneDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GmtOffset { get; set; }
        public string UtcOffset { get; set; } = string.Empty;
    }
}
