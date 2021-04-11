using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.User.Command.SignIn
{
    public class SignInCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, string>
    {
        private readonly IJwtService _jwtService;
        private readonly IUsersService _usersService;

        public SignInCommandHandler(IUsersService usersService, IJwtService jwtService)
        {
            _usersService = usersService;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersService.GetUserByEmailAsync(request.Email);
            if (user == null) throw new NotFoundException("Email address or password wrong!");
            var isPasswordValid = await _usersService.ValidatePasswordAsync(user, request.Password);
            if (!isPasswordValid) throw new NotFoundException("Email address or password wrong!");
            return _jwtService.GenerateToken(user.Id, user.FirstName);
        }
    }
}