using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate authenticate;
        public TokenController(IAuthenticate authenticate)
        {
            this.authenticate = authenticate ?? throw new ArgumentException(nameof(authenticate));
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login ([FromBody] LoginModel useInfo)
        {
            var result = await authenticate.Authenticate(useInfo.Email, useInfo.Password);

            if(result)
            {
                //return GenerateToken(useInfo);
                return Ok ($"User {useInfo.Email} login successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }

    }
}