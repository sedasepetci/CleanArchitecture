using CleanArchitecture.Domain.Entities;
using MediatR;


namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCar;

public sealed record class GetAllCarQuery(
    int PageNumber=1,
    int PageSize=10,
    string Search=""
    ):IRequest<IList<Car>>;
