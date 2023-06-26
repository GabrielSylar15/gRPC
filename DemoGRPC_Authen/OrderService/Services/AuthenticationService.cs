using AccountService;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderService.Services
{
    public class AuthenticationService : AccountAuthen.AccountAuthenBase
    {
        private readonly OrderServiceContext _context = new OrderServiceContext();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(OrderServiceContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public override async Task<Token> Login(AccountRequest request, ServerCallContext context)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (!result.Succeeded)
            {
                return new Token() { JwtToken = String.Empty };
            }
            else
            {
                var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                ApplicationUser user = await _context.Users.Where(u => u.UserName.Equals(request.UserName)).FirstOrDefaultAsync();
                var claims = await GetValidClaims(user);
                var token = new JwtSecurityToken(
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature),
                        expires: DateTime.Now.AddMinutes(100),
                        claims: claims
                    );
                return new Token() { JwtToken = new JwtSecurityTokenHandler().WriteToken(token) };
            }
        }

        public async Task<List<Claim>> GetValidClaims(ApplicationUser user)
        {
            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>();
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userClaims);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            return claims;
        }

        public override async Task<AccountSignin> Signin(AccountSignin request, ServerCallContext context)
        {
            if ("".Equals(request.Password))
            {
                return null;
            }
            try
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    LockoutEnabled = false,
                    LockoutEnd = DateTime.Now,
                    PhoneNumber = "213213"
                };
                IdentityResult rs = await _userManager.CreateAsync(user, request.Password);
                return new AccountSignin() { UserName = request.UserName, Password = request.Password, Email = request.Email };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
