namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

public class EfBodyTrackRepository(DatabaseContext context) : EFBaseRepository<BodyTrack>(context), IBodyTrackRepository
{
}