﻿using BlogPessoal.src.dtos;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Authentication")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        #region Attributes
        private readonly IAuthentication _services;
        #endregion

        #region Constructors
        public AuthenticationController(IAuthentication services)
        {
            _services = services;
        }
        #endregion

        #region Methods
        
        /// <summary>
        /// Get authorization
        /// </summary>
        /// <param name="authentication">AuthenticationDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Example:
        ///
        ///     POST /api/Authentication
        ///     {
        ///        "email": "julio@domain.com",
        ///        "password": "134652"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns created user</response>
        /// <response code="400">Requisition Error</response>
        /// <response code="401">Invalid email or password</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorizationDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Authentication([FromBody] AuthenticationDTO authentication)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var authorization = await _services.GetAuthorizationAsync(authentication);
                return Ok(authorization);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }    
        }

        #endregion Methods
    }
}
