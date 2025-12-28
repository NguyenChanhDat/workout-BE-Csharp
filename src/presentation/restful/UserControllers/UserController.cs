using FirstNETWebApp.UseCase.Base.Interfaces;
using FirstNETWebApp.UseCase.CreateUser.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace FirstNETWebApp.presentation.restful.UserControllers;

[Route("api/user")]
[ApiController]
public class UserController(IMutationUseCase<CreateUserRequest, User> createUserUseCase) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> PostUser(CreateUserRequest createUserRequest)
    {
        var response = await createUserUseCase.ExecuteAsync(createUserRequest);
        return new CreateUserResponse(response.Id, response.Username, response.Email, response.MembershipTier);
    }
    // // GET: api/User
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    // {
    //     return await _context.Users.ToListAsync();
    // }

    // // GET: api/User/5
    // [HttpGet("{id}")]
    // public async Task<ActionResult<User>> GetUser(int id)
    // {
    //     var user = await _context.Users.FindAsync(id);

    //     if (user == null)
    //     {
    //         return NotFound();
    //     }

    //     return user;
    // }

    // // PUT: api/User/5
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutUser(int id, User user)
    // {
    //     if (id != user.Id)
    //     {
    //         return BadRequest();
    //     }

    //     _context.Entry(user).State = EntityState.Modified;

    //     try
    //     {
    //         await _context.SaveChangesAsync();
    //     }
    //     catch (DbUpdateConcurrencyException)
    //     {
    //         if (!UserExists(id))
    //         {
    //             return NotFound();
    //         }
    //         else
    //         {
    //             throw;
    //         }
    //     }

    //     return NoContent();
    // }

    // POST: api/User
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

    // // DELETE: api/User/5
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteUser(int id)
    // {
    //     var user = await _context.Users.FindAsync(id);
    //     if (user == null)
    //     {
    //         return NotFound();
    //     }

    //     _context.Users.Remove(user);
    //     await _context.SaveChangesAsync();

    //     return NoContent();
    // }

    // private bool UserExists(int id)
    // {
    //     return _context.Users.Any(e => e.Id == id);
    // }
}
