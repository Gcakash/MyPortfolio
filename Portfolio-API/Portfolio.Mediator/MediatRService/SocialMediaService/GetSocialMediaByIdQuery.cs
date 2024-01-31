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


namespace Portfolio.API.MediatR.MediatRService.SocialMediaService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SocialMediaId"></param>
    public record GetSocialMediaByIdQuery(int SocialMediaId) : IRequest<ServiceResponse<SocialMediaModel>>;

    public record GetSocialMediaByIdHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetSocialMediaByIdQuery, ServiceResponse<SocialMediaModel>>
    {
        public async Task<ServiceResponse<SocialMediaModel>> Handle(GetSocialMediaByIdQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<SocialMediaModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbSocialMedia = await dbContext.SocialMedias
                    .Where(u => u.Id == request.SocialMediaId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbSocialMedia != null)
                {
                    response.Result = Mapper.Map<SocialMediaModel>(dbSocialMedia);
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
