using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Infrastructure.Entities.DTOs
{
    public class UserDTOs
    {
        public class Create
        {
            public string PhoneNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Update 
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Authenticate
        {
            public string PhoneNumber { get; set; }
            public string code { get; set; }
        }
    }
}
