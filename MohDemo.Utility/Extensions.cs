using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Text;

namespace MohDemo.Utility
{
	public static class DateTimeExtension
	{
		public static DateTime ToDateTime(this string value)
		{
			return Convert.ToDateTime(value);
		}
	}

	public static class SessionExtensions
	{
		public static void SetObjectAsJson(this ISession session, string key, object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T GetObjectFromJson<T>(this ISession session, string key)
		{
			var value = session.GetString(key);

			return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
		}
	}
}