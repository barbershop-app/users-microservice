using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Infrastructure.Entities.DTOs
{
    public class SendSMSDTO
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }

    }
}
