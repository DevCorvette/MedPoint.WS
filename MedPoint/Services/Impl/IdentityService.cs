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

    }
}
