using HomeMade.Application.Interfaces;
using HomeMade.Application.ViewModels;
using HomeMade.Domain.Models;
using HomeMade.Domain.Repositories;
using HomeMade.Security.Authentication;
using System;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _userLoginRepository;
        private readonly ITokenBuilder _tokenBuilder;

        public LoginService(ILoginRepository userLoginRepository, ITokenBuilder tokenBuilder)
        {
            _userLoginRepository = userLoginRepository;
            _tokenBuilder = tokenBuilder;
        }

        public async Task<UserModel> Authenticate(string username, string password)
        {
            // Implement Authentication

            var user = await _userLoginRepository.FetchUser(username);

            if (user == null)
            {
                throw new Exception("InValid userName/Password");
            }

            var token = _tokenBuilder.BuildToken(user.UserName, null, DateTime.Now.AddHours(1));

            var userModel = new UserModel()
            {
                Token = token,
                UserName = user.UserName
            };


            return userModel;

            //if (!VerifyPasswordHash(user.p, user.PasswordHash, user.PasswordSalt))
            //{

            //}
            //var user = ;

            //if (user == null)
            //{
            //    throw new Exception("username/password aren't right");
            //}

            //if (string.IsNullOrWhiteSpace(password) || !user.Password.VerifyWithBCrypt(password))
            //{
            //    throw new Exception("username/password aren't right");
            //}

            //var expiresIn = DateTime.Now + TokenAuthOptions.ExpiresSpan;
            //var token = _tokenBuilder.Build(user.Username, user.Roles.Select(x => x.Role.Name).ToArray(), expiresIn);

            //return new UserWithToken
            //{
            //    ExpiresAt = expiresIn,
            //    Token = token,
            //    User = user
            //};d
        }

        public async Task<string> Register(string userName, string password)
        {
            //Check if user exists
            var checkUser = await _userLoginRepository.FetchUser(userName);

            if (checkUser.UserId > 0)
            {
                throw new Exception();
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserName = userName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var registeredUser = await _userLoginRepository.Register(user);

            var expiresIn = DateTime.Now + TokenAuthOptions.ExpiresSpan;

            var token = _tokenBuilder.BuildToken(registeredUser, null, expiresIn);
            return token;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }
    }
}
