using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Users;
using ToDoApp.Application.CustomExceptions;
using ToDoApp.Application.Users.Repositores;
using ToDoApp.Application.Users.Requests;
using ToDoApp.Application.Users.Responses;

namespace ToDoApp.Application.Users
{
    public class UserService : IUserService
    {
        private const string SECRET_KEY = "lfherffg324";

        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseModel> AuthenticateAsync(CancellationToken cancellationToken,UserRequestModel user)
        {
            var userEntity = user.Adapt<User>();
            userEntity.Password = GenerateHash(userEntity.Password);
            var retrievedUser = await _repository.GetAsync(cancellationToken,userEntity);

            if (retrievedUser == null)
                throw new InvalidUserCredentialsException("Username or password is incorrect", nameof(User));

            return retrievedUser.Adapt<UserResponseModel>();
        }

        public async Task<UserResponseModel> CreateAsync(CancellationToken cancellationToken,UserRequestModel user)
        {
            var userEntity = user.Adapt<User>();
            userEntity.Password = GenerateHash(user.Password);
            var createdUser = await _repository.CreateAsync(cancellationToken, userEntity);

            return createdUser.Adapt<UserResponseModel>();
        }

        private string GenerateHash(string input)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input + SECRET_KEY);
                byte[] hashBytes = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
