using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.API.Common.Constatnt;
using Portfolio.API.Common.Enum;
using Portfolio.API.Common.Models;
using Portfolio.API.Common.ServiceResponse;
using Portfolio.API.Data.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Net;


namespace Portfolio.API.MediatR.MediatRService.WorkExperienceService
{
    // <summary>
    // 
    // </summary>
    // <param name = "WorkExperience" ></ param >
    public record UpdateWorkExperienceCommand(int WorkExperienceId, WorkExperienceModel WorkExperience) : IRequest<ServiceResponse<WorkExperienceModel>>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="DbContext"></param>
    /// <param name="Mediator"></param>
    public record UpdateWorkExperienceHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<UpdateWorkExperienceCommand, ServiceResponse<WorkExperienceModel>>
    {
        public async Task<ServiceResponse<WorkExperienceModel>> Handle(UpdateWorkExperienceCommand request, CancellationToken cancellationToken)
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
                    Mapper.Map(request.WorkExperience, dbWorkExperience);
                    dbContext.Entry(dbWorkExperience).State = EntityState.Modified;
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = request.WorkExperience;
                        response.Type = ServiceResponseTypes.SUCCESS;
                    }
                    else
                    {
                        errors.Add(ErrorConstant.Update_Error);
                        response.Type = ServiceResponseTypes.ERROR;
                        response.ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString();
                        response.Errors = errors;
                    }
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
