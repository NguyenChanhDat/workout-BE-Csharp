namespace FirstNETWebApp.UseCase.CreateUser.Dtos;

public record CreateUserRequest(string Username, string Email, string Password, MembershipTierEnum? MembershipTier);