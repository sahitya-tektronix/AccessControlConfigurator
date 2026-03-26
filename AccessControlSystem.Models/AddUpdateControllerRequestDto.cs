public class AddUpdateControllerRequestDto
{
    public string Name { get; set; }
    public string MacAddress { get; set; }
    public string IpAddress { get; set; }
    public int Port { get; set; }
    public string SubnetMask { get; set; }
    public string DefaultGateway { get; set; }
    public int TimeZoneId { get; set; }
    public int LocationId { get; set; }

    public bool InternalPort0IsEnabled { get; set; }
    public int? InternalPort0BaudRate { get; set; }
    public int? InternalPort0ProtocolType { get; set; }

    public bool Rs485Port1IsEnabled { get; set; }
    public int? Rs485Port1BaudRate { get; set; }
    public int? Rs485Port1ProtocolType { get; set; }

    public bool Rs485Port2IsEnabled { get; set; }
    public int? Rs485Port2BaudRate { get; set; }
    public int? Rs485Port2ProtocolType { get; set; }
    public int Id { get; set; }
}