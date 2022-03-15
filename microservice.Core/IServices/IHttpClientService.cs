using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Core.IServices
{
    public interface IHttpClientService
    {
        /// <summary>
        /// Sends an SMS using the SMS4FREE API
        /// </summary>
        /// <returns></returns>
        Task SendSMS(string phoneNumber, string message);
    }
}
