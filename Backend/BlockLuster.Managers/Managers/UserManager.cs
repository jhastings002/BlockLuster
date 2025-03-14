﻿using BlockLuster.Accessors.Interfaces;
using BlockLuster.Common.SecurityService;
using BlockLuster.Common.Shared.ResponsesAndRequests;
using BlockLuster.EntityFramework;
using BlockLuster.Managers.Interfaces;

namespace BlockLuster.Managers.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserAccessor _userAccessor;
        private readonly ISecurityService _securityService;
        public UserManager(IUserAccessor userAccessor, ISecurityService securityService)
        {
            _userAccessor = userAccessor;
            _securityService = securityService;
        }

        public async Task<SigninResponse> SignUpUserAsync(string firstName, string lastName, string email, string password)
        {
            var user = new AspNetUser
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = email,
                Email = email,
                IsAdmin = false,
                IsDeactivated = false
            };

            await _securityService.SignUpAsync(user, password);
            return await _securityService.SignInAsync(user.Email, password);
            
        }

        public void UpdateProfile(string userId, string firstName, string lastName)
        {
            var user = new AspNetUser
            {
                Id = userId,
                FirstName = firstName,
                LastName = lastName,
            };

            _userAccessor.UpdateUser(user);

        }

        public async Task UpdatePassword(string userId, string oldPassword, string newPassword)
        {
            await _securityService.UpdatePassword(userId, oldPassword, newPassword);

        }

        public void DeactivateUser(string userId)
        {
            var user = _userAccessor.GetUser(userId);
            user.IsDeactivated = true;
            _userAccessor.UpdateUser(user);
        }

        public void ReactivateUser(string userId)
        {
            var user = _userAccessor.GetUser(userId);
            user.IsDeactivated = false;
            _userAccessor.UpdateUser(user);
        }

        public string TestMe(string input)
        {
            return $"{nameof(UserManager)} : {_userAccessor.TestMe(input)}";
        }
    }
}