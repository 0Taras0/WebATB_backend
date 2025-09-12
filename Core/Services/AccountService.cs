using AutoMapper;
using Core.Constants;
using Core.Interfaces;
using Core.Models.Account;
using Domain.Constants;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
    public class AccountService(IJwtTokenService jwtTokenService, UserManager<UserEntity> userManager, IMapper mapper, IImageService imageService, IConfiguration configuration) : IAccountService
    {
        public async Task<AuthResult> RegisterAsync(RegisterModel model)
        {
            var user = mapper.Map<UserEntity>(model);
            if (model.ImageFile != null)
            {
                user.Image = await imageService.SaveImageAsync(model.ImageFile!);
            }

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.User);
                var token = await jwtTokenService.CreateTokenAsync(user);
                return AuthResult.SuccessResult(token);
            }

            return AuthResult.FailureResult("Registration failed");
        }

    }
}
