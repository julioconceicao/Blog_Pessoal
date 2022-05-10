using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogPessoal.src.services.implementations
{
    public class AuthenticationServices : IAuthentication
    {
        #region Attributes
        private readonly IUser _repository;
        public IConfiguration Configuration { get; }

        #endregion

        #region Constructors

        public AuthenticationServices(IUser repository, IConfiguration configuration)
        {
            _repository = repository;
            Configuration = configuration;
        }

        #endregion

        #region Methods
        public string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        public void CreateUserWithoutDuplicate(NewUserDTO dto)
        {
            var user = _repository.GetUserByEmail(dto.Email);

            if (user != null) throw new Exception("This email is already beeing used ;(");

            dto.Password = EncodePassword(dto.Password);

            _repository.NewUser(dto);
        }

        public string GenerateToken(UserModel user)
        {
            var tokenManipulator = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim(ClaimTypes.Role, user.Type.ToString())

                    }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(

                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                        )
            };
            var token = tokenManipulator.CreateToken(tokenDescription);
            return tokenManipulator.WriteToken(token);
        }

        public AuthorizationDTO GetAuthorization(AuthenticationDTO authentication)
        {
            var user = _repository.GetUserByEmail(authentication.Email);

            if (user == null) throw new Exception("User not found :(");

            if (user.Password != EncodePassword(authentication.Password)) throw new
                    Exception("Wrong Password :(");
            return new AuthorizationDTO(user.Id, user.Email, user.Type, GenerateToken(user));
        }
        #endregion
    }
}
