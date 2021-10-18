﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Helpers;
using Models.ViewModels;
using RESTApi.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;

        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager, ITokenService tokenService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task RegisterAsync(RegisterRequest registerRequest)
        {
            if (registerRequest is null)
                throw new Exception("The register request was null.");

            User user = new();

            // Using AutoMapper to copy data from the register request to 
            // the User model
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterRequest, User>();
            });

            IMapper iMapper = config.CreateMapper();
            user = iMapper.Map<RegisterRequest, User>(registerRequest);

            // Create the new user
            var result = await userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
                throw new Exception(result.Errors.FirstOrDefault().ToString());

            // Add to default role
            var roleResult = await userManager.AddToRoleAsync(user, "Customer");
            if (!roleResult.Succeeded)
                throw new Exception(roleResult.Errors.FirstOrDefault().ToString());
        }

        public async Task<string> LoginAsync(LoginRequest loginRequest)
        {
            // Check the credentials
            var result = await signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);
            if (!result.Succeeded)
                throw new Exception("Invalid Credentials.");

            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
                throw new Exception("Invalid Authentication.");

            var token = await tokenService.BuildTokenAsync(user);

            return token;
        }
    }
}