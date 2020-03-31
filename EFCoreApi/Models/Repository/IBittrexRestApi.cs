using System.Threading.Tasks;
using EFCoreApi.Models.DataModel;
using Refit;

namespace EFCoreApi.Models.Repository
{
	/// <summary>
	/// Интерфейс для Refit.
	/// </summary>
	public interface IBittrexRestApi
	{
		[Get("/api/v1.1/public/getmarketsummaries/")]
		Task<CurrencyAggregation> GetData();
	}
}