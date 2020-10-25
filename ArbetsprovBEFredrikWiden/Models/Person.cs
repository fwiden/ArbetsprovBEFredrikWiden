using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArbetsprovBEFredrikWiden.Models
{
    public class Person
    {
        [Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
        public int CompanyID { get; set; }
    }
}