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

        public static int JWT_EXPIRATION_HOURS = 365 * 24;

        public static int CODE_LENGTH = 5;
        public static string CODE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    }
}
