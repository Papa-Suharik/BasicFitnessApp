namespace BasicFitnessApp.CustomExceptionHandler;

public abstract class DomainException : Exception
{
    public DomainException(string message) : base(message){}
    public abstract int StatusCode {get;}
    public abstract string Title {get;}
}

public class SecurityException : DomainException
{
    public SecurityException(string message) : base(message){}
    public override int StatusCode => StatusCodes.Status401Unauthorized;
    public override string Title => "Token rotation breach exception";
}
public class InvariantException : DomainException
{
    public InvariantException(string message) : base(message){}
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "Invariant Violation occured";
}
public class UserNotFoundExcpetion : DomainException
{
    public UserNotFoundExcpetion(string message) : base(message){}
    public override int StatusCode => StatusCodes.Status404NotFound;
    public override string Title => "User not found";
}

public class DuplicateUserProfileException : DomainException
{
    public DuplicateUserProfileException(string message) : base(message){}
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "User already has a profile configured";
}

public class NoUserProfileException : DomainException
{
    public NoUserProfileException(string message) : base(message){}
    public override int StatusCode => StatusCodes.Status404NotFound;
    public override string Title => "No profile is found for this user";
}

public class DuplicateUserException : DomainException
{
    public DuplicateUserException(string message) : base(message){}
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "User already exists";
}
public class AuthenticationFailedException : DomainException
{
    public AuthenticationFailedException(string message) : base(message){}
    public override int StatusCode => StatusCodes.Status401Unauthorized;
    public override string Title => "Login or password is incorrect!";
}
