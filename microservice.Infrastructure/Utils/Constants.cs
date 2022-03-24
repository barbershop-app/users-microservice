using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Infrastructure.Utils
{
    public class Constants
    {
        public const string JWT_SECRET = "YWUzNGdhc2VnZmEzZ2FzZ2FnMw==";
        public static int JWT_EXPIRATION_HOURS = 3;

        public static int CODE_LENGTH = 5;
        public static string CODE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string SMS4FREE_API = "API:sms4free";
        public static string SMS4FREE_KEY = "SsheJ7Ahw";
        public static string SMS4FREE_USER = "0504618885";
        public static string SMS4FREE_PASSWORD = "52454106";
    }
}
