using System;

namespace WebApplication1
{
    public class Smartwatch : Device
    {
        private int _batteryPercentage;
        public int BatteryPercentage
        {
            get => _batteryPercentage;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Battery percentage must be between 0 and 100.");
                _batteryPercentage = value;
            }
        }

        public Smartwatch() : base() { }

        public Smartwatch(string id, string name, bool isTurnedOn, int battery)
            : base(id, name, isTurnedOn)
        {
            BatteryPercentage = battery;
        }

        
        // public override void TurnOn()
        // {
        //     if (BatteryPercentage < 11)
        //         throw new EmptyBatteryException("Cannot turn on. Battery too low.");
        //     BatteryPercentage -= 10;
        //     base.TurnOn();
        // }
        // public void NotifyLowBattery() => Console.WriteLine($"Warning! {Name} battery is low ({BatteryPercentage}%).");
        // public override string GetSaveFormat() => $"{Id},{Name},{IsTurnedOn},{BatteryPercentage}%";
    }
}