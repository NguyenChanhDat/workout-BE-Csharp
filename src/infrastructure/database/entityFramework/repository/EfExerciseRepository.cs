using FirstNETWebApp.Infrastructure.Database.EntityFramework;

public class EfExerciseRepository(DatabaseContext context) : EFBaseRepository<Exercise>(context), IExerciseRepository
{
}