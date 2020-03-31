using System.Collections.Generic;
using Newtonsoft.Json;

namespace EFCoreApi.Models.DataModel
{
	/// <summary>
	/// Модель массива из ответа API.
	/// </summary>
	public class CurrencyAggregation
	{
		[JsonProperty(PropertyName = "result")]
		public List<CurrencyData>? Pairs { get; set; }
	}
}