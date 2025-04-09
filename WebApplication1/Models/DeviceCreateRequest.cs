namespace WebApplication1.Models
{
    public class DeviceCreateRequest
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsTurnedOn { get; set; } = false;

       
        public int? BatteryPercentage { get; set; }
        public string? OperatingSystem { get; set; }
        public string? IPAddress { get; set; }
        public string? NetworkName { get; set; }

        public Device ToDevice() 
        {
            switch (Type.ToLowerInvariant())
            {
                case "smartwatch":
                    if (!BatteryPercentage.HasValue) throw new ArgumentException("BatteryPercentage is required for Smartwatch.");
                    return new Smartwatch(Id, Name, IsTurnedOn, BatteryPercentage.Value);
                case "personalcomputer":
                    return new PersonalComputer(Id, Name, IsTurnedOn, OperatingSystem);
                case "embeddeddevice":
                    if (string.IsNullOrEmpty(IPAddress) || string.IsNullOrEmpty(NetworkName)) throw new ArgumentException("IPAddress and NetworkName are required for EmbeddedDevice.");
                    return new EmbeddedDevice(Id, Name, IsTurnedOn, IPAddress, NetworkName);
                default:
                    throw new ArgumentException($"Invalid device type: {Type}");
            }
        }
    }
}