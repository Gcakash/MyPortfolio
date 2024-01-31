using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using Portfolio.API.Common.ServiceResponse;
using Portfolio.API.Data.ApplicationDbContext;
using Portfolio.API.Common.Enum;
using Portfolio.API.Common.Constatnt;


namespace Portfolio.API.MediatR.MediatRService.SocialMediaService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SocialMediaId"></param>
    public record DeleteSocialMediaCommand(int SocialMediaId) : IRequest<ServiceResponse<bool>>;


    public record DeleteSocialMediaHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory, IMediator Mediator) :
        IRequestHandler<DeleteSocialMediaCommand, ServiceResponse<bool>>
    {
        public async Task<ServiceResponse<bool>> Handle(DeleteSocialMediaCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<bool> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbSocialMedia = await dbContext.SocialMedias
                    .Where(u => u.Id == request.SocialMediaId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (dbSocialMedia != null)
                {
                    dbContext.SocialMedias.Remove(dbSocialMedia); 
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = true;
                        response.Type = ServiceResponseTypes.SUCCESS;
                    }
                    else
                    {
                        errors.Add(ErrorConstant.Delete_Error);
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
