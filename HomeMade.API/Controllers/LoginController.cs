using AutoMapper;
using HomeMade.Api.Models;
using HomeMade.Api.Utility;
using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeMade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtAuthenticationConfig _authenticationConfig = null;
        private readonly ILoginRepository _loginRepository;
        private readonly ITwilioService _twilioService;
        private ILogger<LoginController> _logger;
        public LoginController(ILoginRepository loginRepository, IOptions<JwtAuthenticationConfig> authenticationConfig, ITwilioService twilioService, ILogger<LoginController> logger)
        {
            _loginRepository = loginRepository;
            _authenticationConfig = authenticationConfig.Value;
            _twilioService = twilioService;
            _logger = logger;
        }

        [HttpPost]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(UserModel userModel)
        {
            var appUser = await _loginRepository.GetUser(userModel.MobileNumber, userModel.Password);
            if (appUser == null)
            {
                return StatusCode(403);
            }

            if (!appUser.IsVerified)
            {
                await _twilioService.StartVerificationAsync(userModel.MobileNumberCc, "sms");
                return Ok("IsNotVerified");
            }

            var token = Common.GenerateToken(appUser, _authenticationConfig.SecretKey, _authenticationConfig.Issuer, _authenticationConfig.Audience);
            return Ok(token);
        }

        [HttpPost]
        [Route("VerifyMobileNumber")]
        public async Task<IActionResult> VerifyMobileNumber(MobileVerificationModel verificationModel)
        {
            var result = await _twilioService.CheckVerificationAsync(verificationModel.MobileNumberCc, verificationModel.Code);

            if (result.IsValid)
            {
                if (verificationModel.VerificationReason == "ForgotPassword")
                {
                    return Ok();
                }

                var appUser = await _loginRepository.UpdateIsVerified(verificationModel.MobileNumber);
                var token = Common.GenerateToken(appUser, _authenticationConfig.SecretKey, _authenticationConfig.Issuer, _authenticationConfig.Audience);
                return Ok(token);
            }

            return BadRequest("Verification code doesn't match");
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(UserModel userModel)
        {
            var appUser = await _loginRepository.UpdatePassword(userModel.MobileNumber, userModel.Password);
            var token = Common.GenerateToken(appUser, _authenticationConfig.SecretKey, _authenticationConfig.Issuer, _authenticationConfig.Audience);
            return Ok(token);
        }

        [HttpPost]
        [Route("SendVerificationCode")]
        public async Task<IActionResult> SendVerificationCode(MobileVerificationModel verificationModel)
        {
            var isExistingUser = await _loginRepository.AnyExistingMobileNumber(verificationModel.MobileNumber);
            if (!isExistingUser)
            {
                return BadRequest("Mobile Number is not registered with us");
            }

            var result = await _twilioService.StartVerificationAsync($"+91{verificationModel.MobileNumber}", "sms");
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return Ok();

        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Post(UserModel userModel)
        {
            (bool isExistingUser, string errorMessage)   = await _loginRepository.AnyExistingUser(userModel.UserName, userModel.MobileNumber);
            if (!isExistingUser)
            {
                var appUser = new ApplicationUser
                {
                    Email = userModel.Email,
                    UserName = userModel.UserName,
                    PasswordHash = userModel.Password,
                    MobileNumber = userModel.MobileNumber,
                    UserApartment = new List<UserApartment>() {
                                            new UserApartment() {
                                                ApartmentId = userModel.ApartmentId,
                                                Block = userModel.Block,
                                                ApartmentNumber = userModel.ApartmentNumber,
                                            } },
                    IsVerified = false,
                    UserTypeId = (int)Core.Enums.UserType.CUSTOMER
                };
                
                await _loginRepository.RegisterUser(appUser);
                await _twilioService.StartVerificationAsync(userModel.MobileNumberCc, "sms");
                return Ok();
            }

            return BadRequest(errorMessage);
        }

    }
}
