public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public MembershipTierEnum MembershipTier { get; set; } = MembershipTierEnum.Basic;

    public virtual ICollection<BodyTrack> BodyTracks { get; set; } = new List<BodyTrack>();

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();
}
