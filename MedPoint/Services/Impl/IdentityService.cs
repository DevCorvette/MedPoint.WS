using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedPoint.Data.Entities;
using MedPoint.Data.Models;
using MedPoint.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace MedPoint.Services.Impl
{
    public class IdentityService : IIdentityService
    {
        private IMapper Mapper { get; }
        private UserManager<User> UserManager { get; }
        private AppSettings AppSettings { get; }

        public IdentityService(IMapper mapper, UserManager<User> userManager, IOptions<AppSettings> options)
        {
            Mapper = mapper;
            UserManager = userManager;
            AppSettings = options.Value;
        }

        private UserDto Map(User user)
        {
            return Mapper.Map<UserDto>(user);
        }

        private User Map(UserDto userDto)
        {
            return Mapper.Map<User>(userDto);
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await UserManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new UserNotFoundException("Can't find user by id");
            }

            return Map(user);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException("Can't find user by email");
            }

            return Map(user);
        }

        public async Task<string> RegisterByEmail(RegisterByEmailRequest request)
        {
            var email = request.Email.ToLower();
            try
            {
                await GetUserByEmail(email);
                throw new UserIsAlreadyExistException();
            }
            catch (UserNotFoundException) { }

            var userDto = new UserDto()
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserName = email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Birthday = request.Birthday,
                RegistrationDate = DateTime.UtcNow,
            };
            var user = Map(userDto);

            var result = await UserManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var message = result.Errors.FirstOrDefault()?.Description;
                throw new IdentityServiceException(message);
            }

            var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            return token;
        }

        public async Task<string> ConfirmEmail(string email, string token)
        {
            var user = await GetUserByEmail(email);
            var result = await UserManager.ConfirmEmailAsync(Map(user), token);

            return result.Succeeded ? AppSettings.Urls.ConfirmSuccess : AppSettings.Urls.ConfirmFailure;
        }
    }
}
