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
        private readonly ITokenRepository tokenRepository;

        public AuthRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            ITokenRepository tokenRepository)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenRepository = tokenRepository;
        }

        //Login
        public async Task<LoginResponseDto> LoginAsync(UserLoginRequestDto requestDto)
        {

            var user = await userManager.FindByEmailAsync(requestDto.UserName);


            var isValidUser = await userManager.CheckPasswordAsync(user, requestDto.Password);

            if (isValidUser)
            {
                var roles = await userManager.GetRolesAsync(user);
                // generate JWT

                
                var token = tokenRepository.CreateJWTToken(user, roles.ToList());

                var userDto = new UserDto()
                 {
                     Email = user.Email,
                     Id = Guid.Parse(user.Id),
                     Name = user.Name,
                     PhoneNumber = user.PhoneNumber
                 };

                var loginResponseDto = new LoginResponseDto()
                {
                    User = userDto,
                    JwtToken = token
                };

                return loginResponseDto;

            }

            else
            {
                return new LoginResponseDto() { User = null, JwtToken = "" };
            }

        }
    


        //Register
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
                PhoneNumber =  requestDto.PhoneNumber,

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
                        
                                /*var userToReturn = await dbContext.ApplicationUsers.FirstAsync(u => u.UserName == requestDto.Email);

                                UserDto userDto = new UserDto()
                                {
                                    Email = userToReturn.Email,
                                    Id = Guid.Parse(userToReturn.Id),
                                    Name = userToReturn.Name,
                                    PhoneNumber = userToReturn.PhoneNumber
                                };*/
                               
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

        //Get users by Role
        public async Task<List<UserDto>> GetByRoleAsync(string role)
        {

            //check if role is valid or not
            var roles = await roleManager.Roles.ToListAsync();
            var roleExists = roles.Any(r => r.Name.ToUpperInvariant() == role.ToUpperInvariant());

            try
            {
                if (roleExists)
                {
                    var users = await userManager.GetUsersInRoleAsync(role);

                    var userDtos = users.Select(x =>
                                    new UserDto
                                    {
                                        Name = x.Name,
                                        Email = x.Email,
                                        PhoneNumber = x.PhoneNumber,
                                        Id = Guid.Parse(x.Id),
                                    }).ToList();

                    return userDtos;
                }
            }
            catch (Exception ex)
            { }

            return new List<UserDto>();

        }

        //Get User details by id
        public async Task<UserDto> GetById(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                return new UserDto { Name = user.Name, Email = user.Email, PhoneNumber = user.PhoneNumber, Id = id };
            }
            return new UserDto();
        }
    }

}
