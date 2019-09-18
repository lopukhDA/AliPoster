using System.Configuration;
using Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EpnParser.EpnApi.Entity
{
	public class RequestEpn
	{
		[JsonProperty("user_api_key")]
		public string UserApiKey { get; set; } = ConfigurationManager.AppSettings.Get("user_api_key");

		[JsonProperty("user_hash")]
		public string UserHash { get; set; } = ConfigurationManager.AppSettings.Get("user_hash");

		[JsonProperty("api_version")]
		//[JsonConverter(typeof(JsonExtensions.ParseStringConverter))]
		public string ApiVersion { get; set; } = "2";

		[JsonProperty("requests")]
		public Requests Requests { get; set; }
	}

	public class Requests
	{
		[JsonProperty("request")]
		public Request Request { get; set; }
	}

	public partial class Request
	{
		[JsonProperty("action")]
		[JsonConverter(typeof(StringEnumConverter))]
		public ActionRequest ActionRequest { get; set; }

		[JsonProperty("lang")]
		public Lang Lang { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

	}
}