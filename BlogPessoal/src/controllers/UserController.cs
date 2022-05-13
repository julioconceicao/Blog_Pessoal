using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
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

            [HttpPut, Authorize(Roles = "USER,ADMIN")]
            public async Task<ActionResult> UpDateUserAsync([FromBody] UpDateUserDTO user) 
            {
                if (!ModelState.IsValid) 
                    return BadRequest();
                
                user.Password = _services.EncodePassword(user.Password);

                await _repository.UpDateUserAsync(user);

                return Ok(user);
            }
            
            [HttpDelete("delete/{idUser}"), Authorize(Roles = "ADMIN")]
            public async Task<ActionResult> DeleteUserAsync([FromRoute] int idUser) 
            {
                await _repository.DeleteUserAsync(idUser);

                return NoContent();
            }
            [HttpGet("id/{idUser}"), Authorize(Roles = "USER,ADMIN")]
            public async Task<ActionResult> GetUserByIdAsync([FromRoute] int idUser) 
            {
                var user = await _repository.GetUserByIdAsync(idUser);

                if (user == null) return NotFound();
                
                return Ok(user);
            }

            [HttpGet("email/{userEmail}"), Authorize(Roles = "USER,ADMIN")]
            public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string userEmail) 
            {
                var user = await _repository.GetUserByEmailAsync(userEmail);

                if (user == null) 
                return NotFound();
                
                return Ok(user);
            }
            
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
