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
    public class SecurityController : ControllerBase
    {
        private readonly ITokenService tokenService;

        public SecurityController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        // POST api/<SecurityController>
        [HttpPost]
        public async Task PostAsync([FromBody] RegisterRequest registerRequest)
        {
            await tokenService.CreateTokenAsync(registerRequest);
        }

        // GET api/<SecurityController>
        [HttpGet]
        public async Task<string> GetAsync([FromBody] LoginRequest loginRequest)
        {
            var token = await tokenService.GetTokenAsync(loginRequest);

            return token;
        }
    }
}
