using JWT;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenParser.Model;

namespace TokenParser
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var tokenString = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJhdXRoZW50aWNhdGlvbi1zZXJ2aWNlLXYxLjAiLCJzdWIiOiI0MGM4MTIxMy1lYjA5LTRkN2ItOGZmYi1iOTAzZDg2ZTU0ZWUiLCJhdWQiOiJ3c28yLWVzYiIsImlhdCI6IjIwMTctMDUtMThUMTE6MjY6MDkuNDk4WiIsImV4cCI6IjIwMTctMDYtMThUMTE6MjY6MDkuNDk4WiJ9.DDXS7Vkp1sAkj1bCAl6sq6TiGWskHUXXSfqPZFNhgp8";
            string id = TokenDecoder.Decode(tokenString);
            Console.WriteLine(id);
            Console.ReadKey();
        }
    }
}
