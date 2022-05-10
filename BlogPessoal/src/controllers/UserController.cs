using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Attributes
        private readonly IUser _repository;
        private readonly IAuthentication _services;

        #endregion Attributes

        #region Constructors
        public UserController(IUser repository, IAuthentication services)
        {
            _repository = repository;
            _services = services;
        }
        #endregion Constructors

        #region Methods

        [HttpPost, AllowAnonymous]
        public IActionResult NewUser([FromBody] NewUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                _repository.NewUser(user);
                return Created($"api/Users/email/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut, Authorize(Roles = "NORMAL, ADMIN")]
        public IActionResult UpDateUser([FromBody] UpDateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            user.Password = _services.EncodePassword(user.Password);

            _repository.UpDateUser(user);

            return Ok(user);
        }

        [HttpDelete("delete/{idUser}"), Authorize(Roles = "ADMIN")]
        public IActionResult DeleteUser([FromRoute] int idUser)
        {
            _repository.DeleteUser(idUser);

            return NoContent();
        }

        [HttpGet("id/{idUser}"), Authorize(Roles = "ADMIN")]
        public IActionResult GetUserById([FromRoute] int idUser)
        {
            var user = _repository.GetUserByID(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet("email/{userEmail}"), Authorize(Roles = "ADMIN")]
        public IActionResult GetUserByEmail([FromRoute] string userEmail)
        {
            var user = _repository.GetUserByEmail(userEmail);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetUserByName([FromQuery] string userName)
        {
            var user = _repository.GetUserByName(userName);

            if (user.Count < 1) return NoContent();

            return Ok(user);
        }
        #endregion Methods
    }
}


