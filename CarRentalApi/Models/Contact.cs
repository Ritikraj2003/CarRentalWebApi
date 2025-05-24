using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CarRentalApi.Models
{
    public class Contact: GeneralData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string First { get; set; }
        public string LastName {  get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
        public bool IsDeleted { get; set; } = false;
    }

    public class Address
    {
        public string Location { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }


        


    }
}
