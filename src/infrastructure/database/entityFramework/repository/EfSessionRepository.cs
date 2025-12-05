namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

internal class EfSessionRepository(DatabaseContext context) : EFBaseRepository<Session>(context), ISessionRepository
{
}