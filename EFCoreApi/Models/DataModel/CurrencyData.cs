using System;
using Newtonsoft.Json;

namespace EFCoreApi.Models.DataModel
{
	/// <summary>
	/// Модель данных о торгах для валютной пары.
	/// </summary>
	public class CurrencyData
	{
		[JsonProperty(PropertyName = "MarketName")]
		public string? PairName { get; set; }

		[JsonProperty(PropertyName = "Bid")]
		public decimal? BuyPrice { get; set; }

		[JsonProperty(PropertyName = "Ask")]
		public decimal? SellPrice { get; set; }

		[JsonProperty(PropertyName = "Last")]
		public decimal? LastTradePrice { get; set; }

		[JsonProperty(PropertyName = "High")]
		public decimal? HighPrice { get; set; }

		[JsonProperty(PropertyName = "Low")]
		public decimal? LowPrice { get; set; }

		[JsonProperty(PropertyName = "Volume")]
		public decimal? Volume { get; set; }

		[JsonProperty(PropertyName = "TimeStamp")]
		public DateTime? Updated { get; set; }
	}
}