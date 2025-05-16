using System;
using System.Net;

namespace CarRentalApi.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string First { get; set; }
        public string LastName {  get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
    }

    public class Address
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }


        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }


    }
}
