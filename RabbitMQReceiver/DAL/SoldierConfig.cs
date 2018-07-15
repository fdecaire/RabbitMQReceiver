using Microsoft.EntityFrameworkCore;

namespace RabbitMQReceiver.DAL
{
	public static class SoldierConfig
	{
		public static void AddSoldierConfig(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SoldierRecord>(entity =>
			{
				entity.ToTable("Soldier", "dbo");

				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id)
					.HasColumnName("Id")
					.IsRequired()
					.HasColumnType("GUID");

				entity.Property(e => e.X)
					.HasColumnName("X")
					.HasColumnType("int");

				entity.Property(e => e.Y)
					.HasColumnName("Y")
					.HasColumnType("int");

			});
		}
	}
}
