namespace WebApplication1.Models
{
    public class DeviceUpdateRequest
    {
        public string? Name { get; set; }
        public bool? IsTurnedOn { get; set; }
        public int? BatteryPercentage { get; set; }
        public string? OperatingSystem { get; set; }
        public string? IPAddress { get; set; }
        public string? NetworkName { get; set; }

        public Device ToDevice(string id)
        {
            string type = GetDeviceTypeFromProperties();

            switch (type.ToLowerInvariant())
            {
                case "smartwatch":
                    return new Smartwatch(id, Name ?? "Unknown", IsTurnedOn ?? false, BatteryPercentage ?? 0);
                case "personalcomputer":
                    return new PersonalComputer(id, Name ?? "Unknown", IsTurnedOn ?? false, OperatingSystem);
                case "embeddeddevice":
                    return new EmbeddedDevice(id, Name ?? "Unknown", IsTurnedOn ?? false, IPAddress ?? "0.0.0.0", NetworkName ?? "Unknown Network");
                default:
                    throw new ArgumentException("Could not determine device type from provided properties for update.");
            }
        }

        private string GetDeviceTypeFromProperties()
        {
            if (BatteryPercentage.HasValue) return "Smartwatch";
            if (OperatingSystem != null) return "PersonalComputer";
            if (IPAddress != null || NetworkName != null) return "EmbeddedDevice";
            throw new ArgumentException("Could not determine device type from provided properties.");
        }
    }
}