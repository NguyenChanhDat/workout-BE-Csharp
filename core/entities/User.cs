public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string AvaUrl { get; set; }
    public MembershipTierEnum MembershipTier { get; set; }
    public RoleEnum Role { get; set; }
}