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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserId"></param>
    public record GetActiveUserQuery() : IRequest<ServiceResponse<UserInfoModel>>;

    public record GetActiveUserHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetUserByIdQuery, ServiceResponse<UserInfoModel>>
    {
        public async Task<ServiceResponse<UserInfoModel>> Handle(GetUserByIdQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<UserInfoModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbUserInfo = await dbContext.UserInfos
                    .Where(u => u.IsActive)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbUserInfo != null)
                {
                    response.Result = Mapper.Map<UserInfoModel>(dbUserInfo);
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
