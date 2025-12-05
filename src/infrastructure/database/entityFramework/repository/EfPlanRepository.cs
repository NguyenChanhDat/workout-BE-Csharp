namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

class EfPlanRepository(DatabaseContext context) : EFBaseRepository<Plan>(context), IPlanRepository
{
}