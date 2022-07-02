using AutoMapper;
using Core.Constants;
using Core.DTO.Authentication;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.CustomService;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AuthenticationService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task RegisterAsync(UserRegistrationDTO userRegistrationDTO)
        {
            var user = _mapper.Map<User>(userRegistrationDTO);
            var createUserResult = await _userManager.CreateAsync(user, userRegistrationDTO.Password);

            ExceptionMethods.CheckIdentityResult(createUserResult);

            var roleName = IdentityRoleNames.User.ToString();
            var userRole = await _roleManager.FindByNameAsync(roleName);

            ExceptionMethods.IdentityRoleNullCheck(userRole);

            var addToRoleResult = await _userManager.AddToRoleAsync(user, userRole.Name);

            ExceptionMethods.CheckIdentityResult(addToRoleResult);
        }
    }
}
