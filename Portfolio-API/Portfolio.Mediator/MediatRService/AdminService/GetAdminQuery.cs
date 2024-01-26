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


namespace Portfolio.API.MediatR.MediatRService.AdminService
{
    // <summary>
    // 
    // </summary>
    // <param name = "Admin" ></ param >
    public record GetAdminQuery : IRequest<ServiceResponse<List<AdminModel>>>;

    public record GetAdminHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetAdminQuery, ServiceResponse<List<AdminModel>>>
    {
        public async Task<ServiceResponse<List<AdminModel>>> Handle(GetAdminQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<AdminModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbAdmines = await dbContext.Admins
                    .Where(u => u.IsActive)
                    .ToListAsync(cancellationToken);

                if (dbAdmines.Count > 0)
                {
                    response.Result = Mapper.Map<List<AdminModel>>(dbAdmines);
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
