namespace HasserisWeb
{
    //Private-type customer class, for individuals/families.
    public class Private : Customer
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Private()
        {

        }
        public Private(string Firstname, string Lastname, Address address, ContactInfo contactInfo)
                : base(address, contactInfo)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
        }
    }
}
