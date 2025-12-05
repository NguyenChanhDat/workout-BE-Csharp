namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

public class EfPlanRepository(DatabaseContext context) : EFBaseRepository<Plan>(context), IPlanRepository
{
}