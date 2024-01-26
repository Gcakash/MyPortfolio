using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using Portfolio.API.Common.ServiceResponse;
using Portfolio.API.Data.ApplicationDbContext;
using Portfolio.API.Common.Enum;
using Portfolio.API.Common.Constatnt;


namespace Portfolio.API.MediatR.MediatRService.UserService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserId"></param>
    public record DeleteUserCommand(int UserId) : IRequest<ServiceResponse<bool>>;


    public record DeleteUserHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory, IMediator Mediator) :
        IRequestHandler<DeleteUserCommand, ServiceResponse<bool>>
    {
        public async Task<ServiceResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<bool> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbUserInfo = await dbContext.UserInfos
                    .Where(u => u.UserId == request.UserId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (dbUserInfo != null)
                {
                    dbContext.UserInfos.Remove(dbUserInfo); 
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = true;
                        response.Type = ServiceResponseTypes.SUCCESS;
                    }
                    else
                    {
                        errors.Add(ErrorConstant.User_Delete_Error);
                        response.Result = false;
                        response.Type = ServiceResponseTypes.ERROR;
                        response.ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString();
                        response.Errors = errors;
                    }
                }
                else
                {
                    errors.Add(ErrorConstant.NotFound_Error);
                    response.Result = false;
                    response.Type = ServiceResponseTypes.ERROR;
                    response.ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Errors = errors;
                }
            }
            return response;
        }
    }
}
