using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreApi.Models.ViewModel
{
	public class PairData
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long PairId { get; set; }
		
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