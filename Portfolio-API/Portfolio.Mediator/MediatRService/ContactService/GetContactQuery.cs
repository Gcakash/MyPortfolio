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


namespace Portfolio.API.MediatR.MediatRService.ContactService
{
    // <summary>
    // 
    // </summary>
    // <param name = "Contact" ></ param >
    public record GetContactsQuery : IRequest<ServiceResponse<List<ContactModel>>>;

    public record GetContactsHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetContactsQuery, ServiceResponse<List<ContactModel>>>
    {
        public async Task<ServiceResponse<List<ContactModel>>> Handle(GetContactsQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<ContactModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbContactes = await dbContext.Contacts
                    .Where(u => u.IsActive)
                    .ToListAsync(cancellationToken);

                if (dbContactes.Count > 0)
                {
                    response.Result = Mapper.Map<List<ContactModel>>(dbContactes);
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
