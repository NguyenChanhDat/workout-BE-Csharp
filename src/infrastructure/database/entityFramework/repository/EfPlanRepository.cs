namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

internal class EfPlanRepository(DatabaseContext context) : EFBaseRepository<Plan>(context), IPlanRepository
{
}