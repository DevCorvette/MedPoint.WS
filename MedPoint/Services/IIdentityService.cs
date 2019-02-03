using System;
using System.Threading.Tasks;
using MedPoint.Data.Enums;
using MedPoint.Data.Models;

namespace MedPoint.Services
{
    public interface IIdentityService
    {
        Task<UserDto> GetUserById(Guid id);
        Task<UserDto> GetUserByEmail(string email);
    }
}