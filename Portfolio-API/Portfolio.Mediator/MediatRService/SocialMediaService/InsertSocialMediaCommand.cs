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



namespace Portfolio.API.MediatR.MediatRService.SocialMediaService
{
    public record InsertSocialMediaCommand(SocialMediaModel SocialMedia)
        : IRequest<ServiceResponse<SocialMediaModel>>;

    public record InsertSocialMediaHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<InsertSocialMediaCommand, ServiceResponse<SocialMediaModel>>
    {
        public async Task<ServiceResponse<SocialMediaModel>> Handle(InsertSocialMediaCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<SocialMediaModel> response = new();
            List<string> errors = new();

            if (request.SocialMedia != null)
            {
                using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
                {
                    var dbSocialMedia = Mapper.Map<SocialMedia>(request.SocialMedia);
                    dbContext.Add(dbSocialMedia);
                    var result = await dbContext.SaveChangesAsync(cancellationToken);

                    if (result != 0)
                    {
                        request.SocialMedia.Id = dbSocialMedia.Id;
                        response.Result = request.SocialMedia;
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

