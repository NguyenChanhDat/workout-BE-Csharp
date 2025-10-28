class MockUserRepository : IBaseRepository<User>
{
    public Task<User> Create(User entity)
    {
        return Task.FromResult(entity);
    }

    public Task<User?> Delete(int id)
    {
        return Task.FromResult<User?>(null);
    }

    public Task<List<User>> GetAll()
    {
        var userList = new List<User>
        {
            new User { Id = 1, Username = "Mock User", Password = "password" },
            new User { Id = 2, Username = "Mock User 2", Password = "password" }
        };
        return Task.FromResult(userList);
    }

    public Task<User?> GetOneById(int id)
    {
        return Task.FromResult<User?>(null);
    }

    public Task Update(User entity)
    {
        return Task.CompletedTask;
    }
}