namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

class EfSetRepository(DatabaseContext context) : EFBaseRepository<Set>(context), ISetRepository
{
}