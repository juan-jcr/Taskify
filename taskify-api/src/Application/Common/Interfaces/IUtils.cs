using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IUtils
{
   public string GenerateToken(UserEntity user);
}

