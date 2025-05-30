using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{

   private readonly AppDbContext _context;
   public UserRepository(AppDbContext context)
   {
      _context = context;
   }

   public async Task AddAsync(UserEntity user)
   {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
   }

   public async Task<UserEntity?> GetByEmailAsync(string email)
   {
      return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
   }
}
