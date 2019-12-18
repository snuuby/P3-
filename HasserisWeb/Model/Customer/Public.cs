namespace HasserisWeb
{
    //Public-type customer class, for public work/communial.
    public class Public : Customer
    {
        public string Name { get; set; }
        public string EAN { get; set; }
        public Public()
        {

        }
        public Public(Address address, ContactInfo contactInfo, string Name, string EAN)
                        : base(address, contactInfo)
        {
            this.Name = Name;
            this.EAN = EAN;
        }
    }
}
