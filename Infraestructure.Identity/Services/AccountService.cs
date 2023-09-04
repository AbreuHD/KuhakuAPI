using Core.Application.DTOs.Account;
using Core.Application.DTOs.Email;
using Core.Application.DTOs.General;
using Core.Application.Enum;
using Core.Application.Interface.Repositories;
using Core.Application.Interface.Services;
using Core.Domain.Entities.User;
using Core.Domain.Settings;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserEntityRepository _userEntityRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;


        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IOptions<JWTSettings> jwtSettings,
            IUserEntityRepository userEntityRepository)
        {
            _signInManager = signInManager;
            _emailService = emailService;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _userEntityRepository = userEntityRepository;
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signCredentials
            );
            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new Byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        public async Task<GenericApiResponse<AuthenticationResponse>> Authentication(AuthenticationRequest request)
        {
            var response = new GenericApiResponse<AuthenticationResponse>();
            var User = await _userManager.FindByNameAsync(request.UserName);
            if (User == null)
            {
                response.Success = false;
                response.Message = $"No Account Register with {request.UserName}";
                response.Statuscode = 402;
                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(User.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = $"Invalid Password";
                response.Statuscode = 409;
                return response;
            }
            if (!User.EmailConfirmed)
            {
                response.Success = false;
                response.Message = $"Account not confirm for {request.UserName}";
                response.Statuscode = 400;
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(User);
            response.Payload = new AuthenticationResponse();
            response.Payload.Id = User.Id;
            response.Payload.Name = User.Name;
            response.Payload.LastName = User.LastName;
            response.Payload.Email = User.Email;
            response.Payload.IsVerified = User.EmailConfirmed;
            var roles = await _userManager.GetRolesAsync(User).ConfigureAwait(false);
            response.Payload.Roles = roles.ToList();
            response.Payload.IsVerified = User.EmailConfirmed;
            response.Payload.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Payload.RefreshToken = GenerateRefreshToken().Token;

            return response;
        }

        public async Task<GenericApiResponse<RegisterResponse>> Register(RegisterRequest request, string origin)
        {
            var response = new GenericApiResponse<RegisterResponse>();
            response.Payload = new RegisterResponse();
            var UserNameExist = await _userManager.FindByNameAsync(request.UserName);
            if (UserNameExist != null)
            {
                response.Success = false;
                response.Message = $"Username {request.UserName} is already taken";
                response.Statuscode = 406;
                return response;
            }

            var EmailExist = await _userManager.FindByEmailAsync(request.Email);

            if (EmailExist != null)
            {
                response.Success = false;
                response.Message = $"Email {request.Email} is already registered";
                response.Statuscode = 406;
                return response;
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = "A error occurred trying to register the user.";
                response.Statuscode = 400;
                return response;

            }
            var regiteredUser = await _userManager.FindByEmailAsync(user.Email);
            response.Payload.Id = regiteredUser.Id;
            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
            var verificationUrl = await SendVerificationEMailUrl(user, origin);
            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"Please confirm your account visiting this URL {verificationUrl}",
                Subject = "Confirm registration"
            });

            await _userEntityRepository.AddAsync(new UserEntity()
            {
                Name = request.Name,
                UserID = response.Payload.Id
            });

            return response;
        }

        public async Task<GenericApiResponse<String>> UpdateUser(string userId, RegisterRequest request)
        {
            var response = new GenericApiResponse<String>();
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = "A error occurred trying to register the user.";
                response.Statuscode = 400;
                return response;

            }
            return response;
        }

        private async Task<string> SendVerificationEMailUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Account/EmailConfirm";
            var url = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(url.ToString(), "userId", user.Id);
            verificationUrl = QueryHelpers.AddQueryString(verificationUrl, "token", code);

            return verificationUrl;
        }

        public async Task<string> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"Not account registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return $"An error occurred while confirming {user.Email}";
            }
            return $"Account confirmed for {user.Email}. You can now use the App";

        }

        private async Task<string> SendForgotPasswordUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Account/ResetPassword";
            var url = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(url.ToString(), "token", code);



            return verificationUrl;
        }

        public async Task<GenericApiResponse<String>> ForgotPassword(ForgotPasswordRequest request, string origin)
        {
            var response = new GenericApiResponse<String>();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.Success = false;
                response.Message = $"No account registered with {request.Email}";
                response.Statuscode = 401;
                return response;
            }
            var verificationUrl = await SendForgotPasswordUrl(user, origin);
            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationUrl}",
                Subject = "Reset Password"
            });

            return response;
        }

        public async Task<GenericApiResponse<String>> ResetPassword(ResetPasswordRequest request)
        {
            var response = new GenericApiResponse<String>();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.Success = false;
                response.Message = $"No account registered with {request.Email}";
                response.Statuscode = 401;
                return response;
            }
            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = $"An error occurred while reset password";
                response.Statuscode = 500;
                return response;
            }

            return response;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
