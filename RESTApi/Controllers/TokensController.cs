using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using RESTApi.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService tokenService;

        public TokensController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        // POST api/<TokenController>/register
        [HttpPost("register")]
        public async Task PostAsync([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                await tokenService.CreateTokenAsync(registerRequest);
            }catch (Exception ex)
            {

            }
        }

        // POST api/<TokenController>/login
        [HttpPost("login")]
        public async Task<LoginResponse> PostAsync([FromForm] LoginRequest loginRequest)
        {
            LoginResponse response = new();
            response.Token = await tokenService.GetTokenAsync(loginRequest);

            return response;
        }
    }
}
