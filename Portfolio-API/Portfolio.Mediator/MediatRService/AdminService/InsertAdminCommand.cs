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



namespace Portfolio.API.MediatR.MediatRService.AdminService
{
    public record InsertAdminCommand(AdminModel Admin)
        : IRequest<ServiceResponse<AdminModel>>;

    public record InsertAdminHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<InsertAdminCommand, ServiceResponse<AdminModel>>
    {
        public async Task<ServiceResponse<AdminModel>> Handle(InsertAdminCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<AdminModel> response = new();
            List<string> errors = new();

            if (request.Admin != null)
            {
                using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
                {
                    var dbAdmin = Mapper.Map<Admin>(request.Admin);
                    dbContext.Add(dbAdmin);
                    var result = await dbContext.SaveChangesAsync(cancellationToken);

                    if (result != 0)
                    {
                        request.Admin.Id = dbAdmin.Id;
                        response.Result = request.Admin;
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

