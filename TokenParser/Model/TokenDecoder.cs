using JWT;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenParser.Model
{
	public static class TokenDecoder
	{
		public static string Decode(string tokenString, string secret = "")
		{
			IJsonSerializer serializer = new JsonNetSerializer();
			IDateTimeProvider provider = new UtcDateTimeProvider();
			IJwtValidator validator = new JwtValidator(serializer, provider);
			IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
			IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
			var json = decoder.Decode(tokenString, secret, verify: false);
			Token token = serializer.Deserialize<Token>(json);
			return token.sub;
		}
	}
}
