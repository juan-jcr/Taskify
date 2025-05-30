using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IUserRepository
{
   Task<UserEntity?> GetByEmailAsync(string email);
   Task AddAsync(UserEntity user);
}