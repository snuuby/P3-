namespace HasserisWeb
{
    //Customer is an abstract class, meaning instances of customers has to be either a Private, Public or Business.
    public abstract class Customer
    {

        protected Customer()
        {

        }
        public int ID { get; set; }
        public Address Address { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public int LentBoxes { get; set; }
        public Customer(Address Address, ContactInfo ContactInfo)
        {
            this.Address = Address;
            this.ContactInfo = ContactInfo;


        }


    }


}