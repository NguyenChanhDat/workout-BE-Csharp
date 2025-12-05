namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

public class EfSessionRepository(DatabaseContext context) : EFBaseRepository<Session>(context), ISessionRepository
{
}