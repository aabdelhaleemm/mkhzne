using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.User.Command.Add
{
    public class AddUserCommand : IRequest<bool>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public AddUserCommandHandler(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Entities.User>(request);
            user.UserName = request.Email;
            var newUser = await _usersService.AddUserAsync(user, request.Password);
            if (!newUser.Succeeded) throw new CantAddEntityException(newUser.Errors.First().Description);
            return newUser.Succeeded;
        }
    }
}