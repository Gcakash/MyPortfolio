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
using Portfolio.API.Models;



namespace Portfolio.API.MediatR.MediatRService.UserService
{
    public record InsertUserByAdminCommand(UserInfoModel UserInfo)
        : IRequest<ServiceResponse<UserInfoModel>>;

    public record InsertUserByAdminHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<InsertUserByAdminCommand, ServiceResponse<UserInfoModel>>
    {
        public async Task<ServiceResponse<UserInfoModel>> Handle(InsertUserByAdminCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<UserInfoModel> response = new();
            List<string> errors = new();

            if (request.UserInfo != null)
            {
                using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
                {
                    var dbUser = Mapper.Map<UserInfo>(request.UserInfo);
                    dbContext.Add(dbUser);
                    var result = await dbContext.SaveChangesAsync(cancellationToken);

                    if (result != 0)
                    {
                        request.UserInfo.UserId = dbUser.UserId;
                        response.Result = request.UserInfo;
                        response.Type = ServiceResponseTypes.SUCCESS;
                    }
                    else
                    {
                        errors.Add(ErrorConstant.Insert_Error);
                        response.Type = ServiceResponseTypes.ERROR;
                        response.ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString();
                        response.Errors = errors;
                    }
                }
            }
            else
            {
                errors.Add(ErrorConstant.InvalidInput_Error);
                response.Type = ServiceResponseTypes.BADPARAMETERS;
                response.ErrorCode = ((int)HttpStatusCode.BadRequest).ToString();
                response.Errors = errors;
            }
            return response;

        }
    }
}

