using FirstNETWebApp.Infrastructure.Database.EntityFramework;

internal class EfExerciseRepository(DatabaseContext context) : EFBaseRepository<Exercise>(context), IExerciseRepository
{
}