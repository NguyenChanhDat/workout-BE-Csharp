class ExerciseRepository : EFBaseRepository<Exercise>
{
    public ExerciseRepository(DatabaseContext context) : base(context)
    {
    }
}