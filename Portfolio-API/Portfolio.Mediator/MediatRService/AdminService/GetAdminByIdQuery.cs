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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="AdminId"></param>
    public record GetAdminByIdQuery(int AdminId) : IRequest<ServiceResponse<AdminModel>>;

    public record GetAdminByIdHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetAdminByIdQuery, ServiceResponse<AdminModel>>
    {
        public async Task<ServiceResponse<AdminModel>> Handle(GetAdminByIdQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<AdminModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbAdmin = await dbContext.Admins
                    .Where(u => u.Id == request.AdminId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbAdmin != null)
                {
                    response.Result = Mapper.Map<AdminModel>(dbAdmin);
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
