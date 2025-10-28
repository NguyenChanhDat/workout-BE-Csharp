class PlanRepository : EFBaseRepository<Plan>
{
    public PlanRepository(DatabaseContext context) : base(context)
    {
    }
}