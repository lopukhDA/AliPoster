using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Common.Extensions
{
	public static class JsonExtensions
	{
		public static DateTime? ToDateFromTimestamp(this JToken token)
		{
			var timestampString = token?.Value<string>();
			if (timestampString == null) return null;

			long millis;
			if (!long.TryParse(timestampString, out millis)) return null;
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(millis);
		}

		public static List<T> ToListOfJModels<T>(this JToken token) where T : class
		{
			if (token == null) return null;
			var arr = token as JArray;
			if (arr == null) return null;

			var list = new List<T>();

			foreach (var item in arr)
			{
				list.Add(item.ToJModel<T>());
			}

			return list;
		}

		public static T ToJModel<T>(this JToken token) where T : class
		{
			if (token == null) return null;
			var obj = (T)Activator.CreateInstance(typeof(T), token);
			return obj;
		}

		public static T FromJson<T>(string json) => JsonConvert.DeserializeObject<T>(json, Converter.Settings);
		public static T[] FromArrayJson<T>(string json) => JsonConvert.DeserializeObject<T[]>(json, Converter.Settings);
		public static List<T> FromListJson<T>(string json) => JsonConvert.DeserializeObject<List<T>>(json, Converter.Settings);
		public static string ToJson<T>(this T self) => JsonConvert.SerializeObject(self, Converter.Settings);

		public static class Converter
		{
			public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
			{
				MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
				DateParseHandling = DateParseHandling.None,
				Converters =
				{
					new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
				}
			};
		}

		public class ParseStringConverter : JsonConverter
		{
			public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
			{
				if (reader.TokenType == JsonToken.Null) return null;
				var value = serializer.Deserialize<string>(reader);
				bool b;
				if (bool.TryParse(value, out b))
				{
					return b;
				}
				throw new Exception("Cannot unmarshal type bool");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
			{
				if (untypedValue == null)
				{
					serializer.Serialize(writer, null);
					return;
				}
				var value = (long)untypedValue;
				serializer.Serialize(writer, value.ToString());
			}

			public static readonly ParseStringConverter Singleton = new ParseStringConverter();
		}

		public class ToBoolean : JsonConverter
		{
			public override bool CanWrite { get { return false; } }

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				throw new NotImplementedException();
			}

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
			{
				if (reader.TokenType == JsonToken.Null) return null;
				var value = serializer.Deserialize<string>(reader);
				bool l;
				if (bool.TryParse(value, out l))
				{
					return l;
				}
				throw new Exception("Cannot unmarshal type bool");
			}

			public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);
		}
	}
}