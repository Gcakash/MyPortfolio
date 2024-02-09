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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="WorkExperienceId"></param>
    public record GetWorkExperienceByIdQuery(int WorkExperienceId) : IRequest<ServiceResponse<WorkExperienceModel>>;

    public record GetWorkExperienceByIdHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetWorkExperienceByIdQuery, ServiceResponse<WorkExperienceModel>>
    {
        public async Task<ServiceResponse<WorkExperienceModel>> Handle(GetWorkExperienceByIdQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<WorkExperienceModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbWorkExperience = await dbContext.WorkExperiences
                    .Where(u => u.Id == request.WorkExperienceId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbWorkExperience != null)
                {
                    response.Result = Mapper.Map<WorkExperienceModel>(dbWorkExperience);
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
