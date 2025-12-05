namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

class EfSessionRepository(DatabaseContext context) : EFBaseRepository<Session>(context), ISessionRepository
{
}