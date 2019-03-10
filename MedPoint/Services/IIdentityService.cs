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
        Task<string> RegisterByEmail(RegisterByEmailRequest request);
        Task<string> ConfirmEmail(string email, string token);
    }

    public class RegisterByEmailRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
    }

}