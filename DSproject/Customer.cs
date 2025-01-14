using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSproject
{
    public class Customer
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Customer(int id, string companyName, string contactName, string address, string city, string postalCode, string country)
        {
            Id = id;
            CompanyName = companyName;
            ContactName = contactName;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        public override string ToString()
        {
            return $"Customer(Id={Id}, CompanyName={CompanyName}, ContactName={ContactName})";
        }
    }
}
