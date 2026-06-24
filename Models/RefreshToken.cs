namespace BasicFitnessApp.Models;

public class RefreshToken
{
    public Guid Id {get; set;}
    public Guid UserId {get; set;}
    public Guid? ReplacedWithTokenId {get; set;}
    public string HashedValue {get; set;} = null!;
    public DateTime CreatedAt {get; set;}
    public DateTime ExpiresOn {get ; set;}
    public DateTime? RevokedAt {get ; set;}
    public bool IsRevoked {get; set;}
    public User? User {get; set;}
    public uint Version {get; set;}
    public void Revoke(Guid newTokenId)
    {
        ReplacedWithTokenId = newTokenId;
        RevokedAt = DateTime.UtcNow;
        IsRevoked = true;
    }
}