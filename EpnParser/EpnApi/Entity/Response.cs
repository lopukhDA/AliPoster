using System.Collections.Generic;
using Newtonsoft.Json;

namespace EpnParser.EpnApi.Entity
{
	public class Response
	{
		[JsonProperty("results")]
		public Results Results { get; set; }

		[JsonProperty("identified_as")]
		public string IdentifiedAs { get; set; }
	}

	public class Results
	{
		[JsonProperty("request")]
		public Request Request { get; set; }
	}

	public partial class Request
	{
		[JsonProperty("offers")]
		public List<Offer> Offers { get; set; }

		[JsonProperty("offer")]
		public Offer Offer { get; set; }
	}
}