using AutoMapper;
using Core.Constants;
using Core.DTO.Authentication;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Resources;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IIdentityRoleService _identityRoleService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AuthenticationService(
                IRepository<User> userRepository,
                IIdentityRoleService identityRoleService,
                ITokenService tokenService,
                UserManager<User> userManager,
                RoleManager<IdentityRole> roleManager,
                IMapper mapper
            )
        {
            _userRepository = userRepository;
            _identityRoleService = identityRoleService;
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<UserAutorizationDTO> LoginAsync(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user == null ||
                !await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                throw new HttpException(
                        ErrorMessages.InvalidCredentials,
                        HttpStatusCode.Unauthorized
                    );
            }

            var userRole = await _identityRoleService.GetUserRoleAsync(user);

            return await _tokenService.GenerateUserTokens(user, userRole);
        }

        public async Task RegisterAsync(UserRegistrationDTO userRegistrationDTO)
        {
            var isAlreadyExists = await _userRepository.AnyAsync(
                new UserSpecification.GetByEmail(userRegistrationDTO.Email));

            if (isAlreadyExists)
            {
                throw new HttpException(
                        ErrorMessages.EmailAlreadyExists,
                        HttpStatusCode.BadRequest
                    );
            }

            var user = _mapper.Map<User>(userRegistrationDTO);
            var createUserResult = await _userManager
                                            .CreateAsync(user, userRegistrationDTO.Password);

            ExtensionMethods.CheckIdentityResult(createUserResult);

            var roleName = IdentityRoleNames.User.ToString();
            var userRole = await _roleManager.FindByNameAsync(roleName);

            ExtensionMethods.IdentityRoleNullCheck(userRole);

            var addToRoleResult = await _userManager.AddToRoleAsync(user, userRole.Name);

            ExtensionMethods.CheckIdentityResult(addToRoleResult);
        }
    }
}
