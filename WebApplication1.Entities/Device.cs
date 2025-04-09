namespace WebApplication1
{
    public abstract class Device
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsTurnedOn { get; set; } = false;

        protected Device() { }

        protected Device(string id, string name, bool isTurnedOn)
        {
            Id = id;
            Name = name;
            IsTurnedOn = isTurnedOn;
        }


        //public virtual void TurnOn() => IsTurnedOn = true;
        //public virtual void TurnOff() => IsTurnedOn = false;

    }
}