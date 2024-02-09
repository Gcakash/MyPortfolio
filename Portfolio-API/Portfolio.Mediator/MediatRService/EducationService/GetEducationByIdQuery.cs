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


namespace Portfolio.API.MediatR.MediatRService.EducationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="EducationId"></param>
    public record GetEducationByIdQuery(int EducationId) : IRequest<ServiceResponse<EducationModel>>;

    public record GetEducationByIdHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetEducationByIdQuery, ServiceResponse<EducationModel>>
    {
        public async Task<ServiceResponse<EducationModel>> Handle(GetEducationByIdQuery request, 
            CancellationToken cancellationToken)
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
                    response.Result = Mapper.Map<EducationModel>(dbEducation);
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
