class SessionRepository : EFBaseRepository<Session>
{
    public SessionRepository(DatabaseContext context) : base(context)
    {
    }
}