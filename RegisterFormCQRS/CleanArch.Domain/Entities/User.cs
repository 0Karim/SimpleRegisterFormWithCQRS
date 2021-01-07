using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public ICollection<AddressInfo> AddressList { get; set; }
    }
}
