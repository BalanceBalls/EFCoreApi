using System;

namespace EFCoreApi.Models.DataModel
{
	public class UpdateCurrencyData
	{
		public string? PairName { get; set; }

		public decimal? BuyPrice { get; set; }

		public decimal? SellPrice { get; set; }

		public decimal? LastTradePrice { get; set; }

		public decimal? HighPrice { get; set; }

		public decimal? LowPrice { get; set; }

		public decimal? Volume { get; set; }

		public DateTime? Updated { get; set; }
	}
}