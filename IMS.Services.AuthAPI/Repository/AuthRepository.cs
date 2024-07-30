using IMS.Services.AuthAPI.Data;
using IMS.Services.AuthAPI.Models.Domain;
using IMS.Services.AuthAPI.Models.Dto;
using IMS.Services.AuthAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Services.AuthAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public Task<LoginResponseDto> LoginAsync(UserLoginRequestDto requestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<String> RegisterAsync(UserRegistrationRequestDto requestDto)
        {


            if (requestDto == null)
            {
                return "Request data cannot be null.";
            }

            if (string.IsNullOrEmpty(requestDto.Email) || string.IsNullOrEmpty(requestDto.Password) || string.IsNullOrEmpty(requestDto.Role))
            {
                return "Email, password, and role cannot be null or empty.";
            }

           
            //checking if role entered by user is valid
            var roles = await roleManager.Roles.ToListAsync();
            var roleExists = roles.Any(r => r.Name.ToUpperInvariant() == requestDto.Role.ToUpperInvariant());

            if (!roleExists)
            {
                return "Invalid Role(Allowed:Admin or Customer)";

            }

            var applicationUser = new ApplicationUser
            {
                Name = requestDto.Name,
                UserName = requestDto.Email,
                Email = requestDto.Email,
                PhoneNumber = requestDto.PhoneNumber,

            };

            try
            {
                var identityResult = await userManager.CreateAsync(applicationUser, requestDto.Password);
                if (identityResult.Succeeded)
                {
                             //Add Role to this user
                             identityResult = await userManager.AddToRoleAsync(applicationUser, requestDto.Role);
                            if (identityResult.Succeeded)
                            {
                                /*
                                var userToReturn = await dbContext.ApplicationUsers.FirstAsync(u => u.UserName == requestDto.Email);

                                UserDto userDto = new UserDto()
                                {
                                    Email = userToReturn.Email,
                                    Id = Guid.Parse(userToReturn.Id),
                                    Name = userToReturn.Name,
                                    PhoneNumber = userToReturn.PhoneNumber
                                };
                                */
                                return "";
                            }
                            else
                            {
                                return identityResult.Errors.FirstOrDefault().Description;
                            }
                    
                }
                else
                {
                    return identityResult.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex) { 

                     }

            return "Error Encountered";

        }

   
    }

}
