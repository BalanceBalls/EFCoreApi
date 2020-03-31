using System;
using System.Threading.Tasks;
using EFCoreApi.Helpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EFCoreApi.Tests
{
	[TestFixture]
	public abstract class BaseTest
	{
		protected ApiDbContext TestApiDbContext = default!;
		
		[SetUp]
		public void SetUp()
		{
			var options = new DbContextOptionsBuilder<ApiDbContext>()
				.UseInMemoryDatabase($"{Guid.NewGuid():N}")
				.Options;
			
			TestApiDbContext = new ApiDbContext(options);
		}

		[TearDown]
		public ValueTask TearDown()
		{
			return TestApiDbContext.DisposeAsync();
		}
	}
}