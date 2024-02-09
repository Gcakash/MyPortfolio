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
    // <summary>
    // 
    // </summary>
    // <param name = "Education" ></ param >
    public record GetActiveEducationsQuery : IRequest<ServiceResponse<List<EducationModel>>>;

    public record GetActiveEducationsHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetActiveEducationsQuery, ServiceResponse<List<EducationModel>>>
    {
        public async Task<ServiceResponse<List<EducationModel>>> Handle(GetActiveEducationsQuery request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<EducationModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbEducationes = await dbContext.Educations
                    .Where(u => u.IsActive)
                    .ToListAsync(cancellationToken);

                if (dbEducationes.Count > 0)
                {
                    response.Result = Mapper.Map<List<EducationModel>>(dbEducationes);
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
