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


namespace Portfolio.API.MediatR.MediatRService.EducationService
{
    // <summary>
    // 
    // </summary>
    // <param name = "Education" ></ param >
    public record UpdateEducationCommand(int EducationId, EducationModel Education) : IRequest<ServiceResponse<EducationModel>>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="DbContext"></param>
    /// <param name="Mediator"></param>
    public record UpdateEducationHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<UpdateEducationCommand, ServiceResponse<EducationModel>>
    {
        public async Task<ServiceResponse<EducationModel>> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<EducationModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbEducation = await dbContext.Educations
                    .Where(u => u.Id == request.EducationId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbEducation != null)

                {
                    Mapper.Map(request.Education, dbEducation);
                    dbContext.Entry(dbEducation).State = EntityState.Modified;
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = request.Education;
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
