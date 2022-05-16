using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers 
{
        [ApiController]
        [Route("api/Users")]
        [Produces("application/json")]
        public class UserController: ControllerBase 
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

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user">NewUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Example:
        ///
        ///     POST /api/Users
        ///     {
        ///        "name": "Julio César",
        ///        "email": "julio@domain.com",
        ///        "password": "134652",
        ///        "picture": "picURL",
        ///        "type": "USER"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns created user</response>
        /// <response code="400">Requisition error</response>
        /// <response code="401">Email already in using</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> NewUserAsync([FromBody] NewUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();
                    
            try 
            {
                await _repository.NewUserAsync(user);
                        
                return Created($"api/Users/email/{user.Email}", user);
            } 
                catch (Exception ex) 
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">UpDateUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Example:
        ///
        ///     PUT /api/Users
        ///     {
        ///        "id": 1,    
        ///        "name": "Julio César",
        ///        "password": "134652",
        ///        "picture": "picURL"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated user</response>
        /// <response code="400">Requisition error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut, Authorize(Roles = "USER,ADMIN")]
        public async Task<ActionResult> UpDateUserAsync([FromBody] UpDateUserDTO user) 
        {
            if (!ModelState.IsValid) 
                return BadRequest();
                
            user.Password = _services.EncodePassword(user.Password);

            await _repository.UpDateUserAsync(user);

            return Ok(user);
        }

        /// <summary>
        /// Delete user by ID
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Deleted user</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]    
        [HttpDelete("delete/{idUser}"), Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int idUser) 
        {
            await _repository.DeleteUserAsync(idUser);

            return NoContent();
        }
        
        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns user</response>
        /// <response code="404">User not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idUser}"), Authorize(Roles = "USER,ADMIN")]
            public async Task<ActionResult> GetUserByIdAsync([FromRoute] int idUser) 
            {
                var user = await _repository.GetUserByIdAsync(idUser);

                if (user == null) return NotFound();
                
                return Ok(user);
            }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="userEmail">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns user</response>
        /// <response code="404">Email not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("email/{userEmail}"), Authorize(Roles = "USER,ADMIN")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string userEmail) 
        {
            var user = await _repository.GetUserByEmailAsync(userEmail);

            if (user == null) 
            return NotFound();
                
            return Ok(user);
        }
            
        /// <summary>
        /// Get user by name
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns user</response>
        /// <response code="204">User not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet, Authorize(Roles = "USER,ADMIN")]
        public async Task<ActionResult> GetUserByNameAsync([FromQuery] string userName) 
        {
            var user = await _repository.GetUserByNameAsync(userName);

            if (user.Count < 1) 
                return NoContent();
                
                return Ok(user);
        }
        
        #endregion Methods
    }
}