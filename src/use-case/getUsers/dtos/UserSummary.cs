namespace FirstNETWebApp.UseCase.GetUsers.Dtos;

public record UserSummary(int Id, string Username, string Email, MembershipTierEnum MembershipTier);
