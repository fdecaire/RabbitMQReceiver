using Microsoft.EntityFrameworkCore;

namespace RabbitMQReceiver.DAL
{
    public interface IDatabaseContext
    {
	    DbSet<SoldierRecord> SoldierRecords { get; set; }
	    void SaveChanges();
    }
}
