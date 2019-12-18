namespace HasserisWeb
{
    public class Vehicle : Equipment
    {
        public string Model { get; set; }
        public string RegNum { get; set; }
        public Vehicle(string name, string model, string regNum) : base(name)
        {
            this.Model = model;
            this.RegNum = regNum;
        }
        public Vehicle()
        {
        }

    }
}
