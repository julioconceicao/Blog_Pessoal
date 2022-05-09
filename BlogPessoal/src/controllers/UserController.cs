using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]

    public class UserController : ControllerBase
    {
        #region Attributes
        private readonly IUser _repository;

        #endregion Attributes

        #region Constructors
        public UserController(IUser repository)
        {
            _repository = repository;
        }

        #endregion Constructors

        #region Methods

        [HttpGet("id/{idUser}")]
        public IActionResult GetUserById([FromRoute] int idUser)
        {
            var user = _repository.GetUserByID(idUser);

            if (user == null) return NotFound();
            
            return Ok(user);
        }

        [HttpPost]
        public IActionResult NewUser([FromBody] NewUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.NewUser(user);
            return Created($"api/Users/{user.Email}", user);
        }

        [HttpPut]
        public IActionResult UpDateUser([FromBody] UpDateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.UpDateUser(user);
            return Ok(user);
        }

        [HttpDelete("delete/{idUser}")]
        public IActionResult DeleteUser([FromRoute] int idUser)
        {
            _repository.DeleteUser(idUser);
            return NoContent();
        }

        #endregion Methods
    }
}

