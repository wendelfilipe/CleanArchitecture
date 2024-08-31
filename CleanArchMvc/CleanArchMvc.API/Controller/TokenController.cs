using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchMvc.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate authenticate;
        private readonly IConfiguration configuration;
        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            this.authenticate = authenticate ?? throw new ArgumentException(nameof(authenticate));
            this.configuration = configuration;
        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        public async Task<ActionResult> CreateUser(LoginModel userInfo)
        {
            var result = await authenticate.RegisterUser(userInfo.Email, userInfo.Password);

            if(result)
            {
                return Ok($"User {userInfo.Email} was created successfuly");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt");
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel useInfo)
        {
            var result = await authenticate.Authenticate(useInfo.Email, useInfo.Password);

            if(result)
            {
                return GenerateToken(useInfo);
                // return Ok ($"User {useInfo.Email} login successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(LoginModel useInfo)
        {
            // declaração do usuário
            var claims = new[]
            {
                new Claim("email", useInfo.Email),
                new Claim("valorDaCahve", "o que voce quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // gerar chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));

            // gerar a assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            // definir o tempo de expiração 
            var expiration = DateTime.UtcNow.AddMinutes(10);

            // gerar token
            JwtSecurityToken token = new JwtSecurityToken(
                // emissor
                issuer: configuration["Jwt:Issuer"],
                // audience
                audience: configuration["Jwt:Audience"],
                // claims
                claims: claims,
                // data de expiração
                expires: expiration,
                // assinatura digital
                signingCredentials: credentials
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

    }
}