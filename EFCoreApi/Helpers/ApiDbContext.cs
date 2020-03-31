using EFCoreApi.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApi.Helpers
{
	/// <summary>
	/// Контекст подключения к EFCore БД.
	/// </summary>
	public class ApiDbContext : DbContext
	{
		public ApiDbContext(DbContextOptions dbContextOptions)
			: base(dbContextOptions)
		{ }

		public DbSet<PairData> Pairs { get; set; } = default!;
	}
}