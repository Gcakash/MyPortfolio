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


namespace Portfolio.API.MediatR.MediatRService.WorkExperienceService
{
    // <summary>
    // 
    // </summary>
    // <param name = "WorkExperience" ></ param >
    public record GetWorkExperiencesQuery : IRequest<ServiceResponse<List<WorkExperienceModel>>>;

    public record GetWorkExperiencesHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetWorkExperiencesQuery, ServiceResponse<List<WorkExperienceModel>>>
    {
        public async Task<ServiceResponse<List<WorkExperienceModel>>> Handle(GetWorkExperiencesQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<WorkExperienceModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbWorkExperiencees = await dbContext.WorkExperiences
                    .ToListAsync(cancellationToken);

                if (dbWorkExperiencees.Count > 0)
                {
                    response.Result = Mapper.Map<List<WorkExperienceModel>>(dbWorkExperiencees);
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
