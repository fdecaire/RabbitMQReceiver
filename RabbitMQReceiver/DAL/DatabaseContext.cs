using Microsoft.EntityFrameworkCore;

namespace RabbitMQReceiver.DAL
{
    public class DatabaseContext : DbContext, IDatabaseContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options)
			: base(options)
		{

		}

		public DbSet<SoldierRecord> SoldierRecords { get; set; }

		public new void SaveChanges()
		{
			base.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.AddSoldierConfig();
		}
	}
}
