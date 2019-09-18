using System;
using System.Collections.Generic;
using Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EpnParser.EpnApi.Entity
{
	public class Offer
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("id_category")]
		//[JsonConverter(typeof(JsonExtensions.ParseStringConverter))]
		public string IdCategory { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("price")]
		public double Price { get; set; }

		[JsonProperty("currency")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Currency Currency { get; set; }

		[JsonProperty("product_id")]
		public string ProductId { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("lang")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Lang Lang { get; set; }

		[JsonProperty("picture")]
		public Uri Picture { get; set; }

		[JsonProperty("all_images")]
		public List<Uri> AllImages { get; set; }

		[JsonProperty("commission_rate")]
		public string CommissionRate { get; set; }

		[JsonProperty("prices")]
		public Dictionary<string, double> Prices { get; set; }

		[JsonProperty("url")]
		public Uri Url { get; set; }
	}
}