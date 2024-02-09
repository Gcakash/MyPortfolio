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



namespace Portfolio.API.MediatR.MediatRService.WorkExperienceService
{
    public record InsertWorkExperienceCommand(WorkExperienceModel WorkExperience)
        : IRequest<ServiceResponse<WorkExperienceModel>>;

    public record InsertWorkExperienceHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<InsertWorkExperienceCommand, ServiceResponse<WorkExperienceModel>>
    {
        public async Task<ServiceResponse<WorkExperienceModel>> Handle(InsertWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<WorkExperienceModel> response = new();
            List<string> errors = new();

            if (request.WorkExperience != null)
            {
                using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
                {
                    var dbWorkExperience = Mapper.Map<WorkExperience>(request.WorkExperience);
                    dbContext.Add(dbWorkExperience);
                    var result = await dbContext.SaveChangesAsync(cancellationToken);

                    if (result != 0)
                    {
                        request.WorkExperience.Id = dbWorkExperience.Id;
                        response.Result = request.WorkExperience;
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

