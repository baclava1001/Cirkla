﻿using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Users;

namespace Cirkla_API.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;


        // TODO: Add some error handling and logging
        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateProfile(User user)
        {
            await _userRepository.Create(user);
            await _userRepository.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteProfile(string id)
        {
            User user = await _userRepository.Get(id);
            await _userRepository.Delete(user);
            await _userRepository.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateProfile(string id, User user)
        {
            if (id == user.Id)
            {
                await _userRepository.Update(user);
                await _userRepository.SaveChanges();
            }
            return true;
        }
    }
}
