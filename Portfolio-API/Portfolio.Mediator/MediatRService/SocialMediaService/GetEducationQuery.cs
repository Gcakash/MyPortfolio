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
    // <summary>
    // 
    // </summary>
    // <param name = "SocialMedia" ></ param >
    public record GetSocialMediasQuery : IRequest<ServiceResponse<List<SocialMediaModel>>>;

    public record GetSocialMediasHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetSocialMediasQuery, ServiceResponse<List<SocialMediaModel>>>
    {
        public async Task<ServiceResponse<List<SocialMediaModel>>> Handle(GetSocialMediasQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<SocialMediaModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbSocialMediaes = await dbContext.SocialMedias
                    .ToListAsync(cancellationToken);

                if (dbSocialMediaes.Count > 0)
                {
                    response.Result = Mapper.Map<List<SocialMediaModel>>(dbSocialMediaes);
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
