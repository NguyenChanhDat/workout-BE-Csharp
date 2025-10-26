class UserRepository : EFBaseRepository<User>
{
    public UserRepository(DatabaseContext context) : base(context)
    {
    }
}