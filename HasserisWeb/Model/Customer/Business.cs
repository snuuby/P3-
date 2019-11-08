using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Business-type class, for private companies/corporations
    public class Business : Customer
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CVR { get; set; }

        public Business()
        {

        }
        public Business(Address Address, ContactInfo ContactInfo, string Name, string CVR )
                        : base(Address, ContactInfo)
        {
            this.Name = Name;
            this.CVR = CVR;
        }

    }
}
