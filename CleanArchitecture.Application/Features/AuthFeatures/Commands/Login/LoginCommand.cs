using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;

public sealed record CreateNewTokenByRefreshCommand(
    string UserNameOrEmail,
    string Password
    ):IRequest<LoginCommandResponse>;

