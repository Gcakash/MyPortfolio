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


namespace Portfolio.API.MediatR.MediatRService.AdminService
{
    // <summary>
    // 
    // </summary>
    // <param name = "Admin" ></ param >
    public record UpdateAdminCommand(int AdminId, AdminModel Admin) : IRequest<ServiceResponse<AdminModel>>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="DbContext"></param>
    /// <param name="Mediator"></param>
    public record UpdateAdminHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<UpdateAdminCommand, ServiceResponse<AdminModel>>
    {
        public async Task<ServiceResponse<AdminModel>> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
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
                    Mapper.Map(request.Admin, dbAdmin);
                    dbContext.Entry(dbAdmin).State = EntityState.Modified;
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = request.Admin;
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
