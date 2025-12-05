namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

internal class EfBodyTrackRepository(DatabaseContext context) : EFBaseRepository<BodyTrack>(context), IBodyTrackRepository
{
}