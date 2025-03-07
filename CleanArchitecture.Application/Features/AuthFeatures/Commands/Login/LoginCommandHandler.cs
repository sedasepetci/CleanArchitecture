using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Login
{
    public sealed class LoginCommandHandler : IRequestHandler<CreateNewTokenByRefreshCommand, LoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginCommandResponse> Handle(CreateNewTokenByRefreshCommand request,CancellationToken cancellationToken)
        {
            LoginCommandResponse response = await _authService.LoginAsync(request, cancellationToken);

            return response;
        }
        
        
    }

}
