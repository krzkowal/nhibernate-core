﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Linq;
using NHibernate.Cfg;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibernate.Test.Linq
{
	using System.Threading.Tasks;
	[TestFixture]
	public class QueryFlushModeTestsAsync : LinqTestCase
	{
		protected override void Configure(Configuration configuration)
		{
			configuration.SetProperty(Environment.GenerateStatistics, "true");
			base.Configure(configuration);
		}

		[Test]
		public async Task CanSetFlushModeOnQueriesAsync(
			[Values(FlushMode.Always, FlushMode.Auto, FlushMode.Commit, FlushMode.Manual)]
			FlushMode flushMode)
		{
			Sfi.Statistics.Clear();

			using (var t = session.BeginTransaction())
			{
				var customer = await (db.Customers.FirstAsync());
				customer.CompanyName = "Blah";

				var unused =
					await (db.Customers
					  .Where(c => c.CompanyName == "Bon app'")
					  .WithOptions(o => o.SetFlushMode(flushMode))
					  .ToListAsync());

				var expectedFlushCount = 0;
				switch (flushMode)
				{
					case FlushMode.Always:
					case FlushMode.Auto:
						expectedFlushCount++;
						break;
				}
				Assert.That(Sfi.Statistics.FlushCount, Is.EqualTo(expectedFlushCount), "Unexpected flush count on same entity query");

				customer.CompanyName = "Other blah";

				var dummy =
					await (db.Orders
					  .Where(o => o.OrderId > 10)
					  .WithOptions(o => o.SetFlushMode(flushMode))
					  .ToListAsync());

				switch (flushMode)
				{
					case FlushMode.Always:
						expectedFlushCount++;
						break;
				}
				Assert.That(Sfi.Statistics.FlushCount, Is.EqualTo(expectedFlushCount), "Unexpected flush count on other entity query");

				// Tests here should not alter data, LinqTestCase derives from ReadonlyTestCase
				await (t.RollbackAsync());
			}
		}

		[Test]
		public async Task CanSetCommentOnPagingQueryAsync(
			[Values(FlushMode.Always, FlushMode.Auto, FlushMode.Commit, FlushMode.Manual)]
			FlushMode flushMode)
		{
			Sfi.Statistics.Clear();

			using (var t = session.BeginTransaction())
			{
				var customer = await (db.Customers.FirstAsync());
				customer.CompanyName = "Blah";

				var unused =
					await (db.Customers
					  .Skip(1).Take(1)
					  .WithOptions(o => o.SetFlushMode(flushMode))
					  .ToListAsync());

				var expectedFlushCount = 0;
				switch (flushMode)
				{
					case FlushMode.Always:
					case FlushMode.Auto:
						expectedFlushCount++;
						break;
				}
				Assert.That(Sfi.Statistics.FlushCount, Is.EqualTo(expectedFlushCount), "Unexpected flush count on same entity query");

				customer.CompanyName = "Other blah";

				var dummy =
					await (db.Orders
					  .Skip(1).Take(1)
					  .WithOptions(o => o.SetFlushMode(flushMode))
					  .ToListAsync());

				switch (flushMode)
				{
					case FlushMode.Always:
						expectedFlushCount++;
						break;
				}
				Assert.That(Sfi.Statistics.FlushCount, Is.EqualTo(expectedFlushCount), "Unexpected flush count on other entity query");

				// Tests here should not alter data, LinqTestCase derives from ReadonlyTestCase
				await (t.RollbackAsync());
			}
		}

		[Test]
		public async Task CanSetCommentBeforeSkipOnOrderedPageQueryAsync(
			[Values(FlushMode.Always, FlushMode.Auto, FlushMode.Commit, FlushMode.Manual)]
			FlushMode flushMode)
		{
			Sfi.Statistics.Clear();

			using (var t = session.BeginTransaction())
			{
				var customer = await (db.Customers.FirstAsync());
				customer.CompanyName = "Blah";

				var unused =
					await (db.Customers
					  .OrderBy(c => c.CompanyName)
					  .Skip(5).Take(5)
					  .WithOptions(o => o.SetFlushMode(flushMode))
					  .ToListAsync());

				var expectedFlushCount = 0;
				switch (flushMode)
				{
					case FlushMode.Always:
					case FlushMode.Auto:
						expectedFlushCount++;
						break;
				}
				Assert.That(Sfi.Statistics.FlushCount, Is.EqualTo(expectedFlushCount), "Unexpected flush count on same entity query");

				customer.CompanyName = "Other blah";

				var dummy =
					await (db.Orders
					  .OrderBy(o => o.OrderId)
					  .Skip(5).Take(5)
					  .WithOptions(o => o.SetFlushMode(flushMode))
					  .ToListAsync());

				switch (flushMode)
				{
					case FlushMode.Always:
						expectedFlushCount++;
						break;
				}
				Assert.That(Sfi.Statistics.FlushCount, Is.EqualTo(expectedFlushCount), "Unexpected flush count on other entity query");

				// Tests here should not alter data, LinqTestCase derives from ReadonlyTestCase
				await (t.RollbackAsync());
			}
		}
	}
}