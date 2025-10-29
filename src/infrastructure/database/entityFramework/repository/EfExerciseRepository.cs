public class EfExerciseRepository(DatabaseContext context) : EFBaseRepository<Exercise>(context), IExerciseRepository
{
}