using System;
using System.Threading.Tasks;

namespace EFCoreApi.Helpers
{
	public interface IUpdateDataJob
	{
		/// <summary>
		/// Выполняет задачу.
		/// </summary>
		Task RunAtTimeOf(DateTime now);
	}
}