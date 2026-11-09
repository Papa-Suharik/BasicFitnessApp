namespace BasicFitnessApp.Dto;

public class LoginDto
{
    public string Email {get; set;} = null!;
    public string Password {get; set;} = null!;
}
public class LoginResultDto
{
    public string JwtToken {get; set;} = null!;
    public string RefreshToken {get; set;} = null!;
    public Guid Id {get; set;}
}

public class RefreshDto
{
    public string RawToken {get; set;} = null!;
    public Guid Id {get; set;}
}