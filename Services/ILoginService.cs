using BasicFitnessApp.Dto;

namespace BasicFitnessApp.Services;

public interface ILoginService
{
    Task<LoginResultDto> Login(LoginDto dto);
    Task<LoginResultDto> RefreshTokens(RefreshDto dto);
}