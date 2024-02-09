using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using Portfolio.API.Common.ServiceResponse;
using Portfolio.API.Data.ApplicationDbContext;
using Portfolio.API.Common.Enum;
using Portfolio.API.Common.Constatnt;
using Portfolio.API.Common.Models;
using AutoMapper;


namespace Portfolio.API.MediatR.MediatRService.UserService
{
    // <summary>
    // 
    // </summary>
    // <param name = "UserInfo" ></ param >
    public record GetUsersQuery : IRequest<ServiceResponse<List<UserInfoModel>>>;

    public record GetUsersHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetUsersQuery, ServiceResponse<List<UserInfoModel>>>
    {
        public async Task<ServiceResponse<List<UserInfoModel>>> Handle(GetUsersQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<UserInfoModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbUserInfoes = await dbContext.UserInfos
                    .Where(u => u.IsActive)
                    .ToListAsync(cancellationToken);

                if (dbUserInfoes.Count > 0)
                {
                    response.Result = Mapper.Map<List<UserInfoModel>>(dbUserInfoes);
                    response.Type = ServiceResponseTypes.SUCCESS;
                }
                else
                {
                    errors.Add(ErrorConstant.NotFound_Error);
                    response.Type = ServiceResponseTypes.NOTFOUND;
                    response.ErrorCode = ((int)HttpStatusCode.NotFound).ToString();
                    response.Errors = errors;
                }
            }
            return response;
        }
    }
}
