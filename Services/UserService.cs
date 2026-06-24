using BasicFitnessApp.Models;
using BasicFitnessApp.Repo;
using BasicFitnessApp.Dto;
using BasicFitnessApp.Mappers;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using BasicFitnessApp.CustomExceptionHandler;
using System.Xml;

namespace BasicFitnessApp.Services;

public class UserService(IUserRepo repo, IPasswordHasher<User> hasher) : IUserService
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await repo.GetByEmailAsync(email, cancellationToken);
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await repo.GetByIdAsync(id, cancellationToken) ?? throw new UserNotFoundExcpetion($"No user found under id- {id}");

        return user.ToUserDto();
    }
    public async Task<UserWithProfileDto> GetUserByIdWithProfileAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await repo.GetByIdWithProfileAsync(id, cancellationToken) ?? throw new UserNotFoundExcpetion($"No user found under id- {id}");

        if(user.Profile is null)
        {
            throw new NoUserProfileException($"No profile is configured for user id: \"{id}\"");
        }

        return user.ToUserWithProfileDto();
    }
    public async Task<UserWithProfileDto> CreateUserProfile(Guid id, UserProfileDto dto, CancellationToken cancellationToken)
    {
        var user = await repo.GetByIdWithProfileAsync(id, cancellationToken) ?? throw new UserNotFoundExcpetion($"No user found under id- {id}");
        
        if(user.Profile != null)
        {
            throw new DuplicateUserProfileException($"Profile for user id: \"{id}\" is already configured");
        }
        
        user.SetProfile(dto.Name, dto.Age, dto.Height, dto.Weight, dto.Gender, dto.Goal, dto.Experience);

        await repo.SaveAsync(cancellationToken);

        return user.ToUserWithProfileDto();
    }
    public async Task<UserWithProfileDto> UpdateUserProfile(Guid id, UserProfileUpdateDto dto, CancellationToken cancellationToken)
    {
        var user = await repo.GetByIdWithProfileAsync(id, cancellationToken) ?? throw new UserNotFoundExcpetion($"No user found under id- {id}");

        if(user.Profile is null)
        {
            throw new NoUserProfileException($"No profile is configured for user id: \"{id}\"");
        }

        user.Profile.UpdateProfile(dto.Name, dto.Age, dto.Height, dto.Weight, dto.Gender, dto.Goal, dto.Experience);

        await repo.SaveAsync(cancellationToken);

        return user.ToUserWithProfileDto();
    }
    public async Task<UserDto> AddUserAsync(UserRegistrationDto dto, CancellationToken cancellationToken)
    {
        var result = await GetUserByEmailAsync(dto.Email, cancellationToken);

        if(result != null)
        {
            throw new DuplicateUserException("User already exists!");
        }

        string hashedPassword = hasher.HashPassword(null!, dto.Password);

        var userToAdd = new User(dto.Email, hashedPassword);

        var user = await repo.AddAsync(userToAdd, cancellationToken);

        await repo.SaveAsync(cancellationToken);

        return user.ToUserDto();
    }
}