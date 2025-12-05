namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

internal class EfSetRepository(DatabaseContext context) : EFBaseRepository<Set>(context), ISetRepository
{
}