using BasicFitnessApp.Dto;
using BasicFitnessApp.Models;

namespace BasicFitnessApp.Services;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<UserWithProfileDto> GetUserByIdWithProfileAsync(Guid id, CancellationToken cancellationToken);
    Task<UserWithProfileDto> CreateUserProfile(Guid id, UserProfileDto dto, CancellationToken cancellationToken);
    Task<UserWithProfileDto> UpdateUserProfile(Guid id, UserProfileUpdateDto dto, CancellationToken cancellationToken);
    Task<UserDto> AddUserAsync(UserRegistrationDto dto, CancellationToken cancellationToken);
}