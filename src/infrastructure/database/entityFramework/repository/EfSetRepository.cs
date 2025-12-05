namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

public class EfSetRepository(DatabaseContext context) : EFBaseRepository<Set>(context), ISetRepository
{
}