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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ContactId"></param>
    public record GetContactByIdQuery(int ContactId) : IRequest<ServiceResponse<ContactModel>>;

    public record GetContactByIdHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetContactByIdQuery, ServiceResponse<ContactModel>>
    {
        public async Task<ServiceResponse<ContactModel>> Handle(GetContactByIdQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<ContactModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbContact = await dbContext.Contacts
                    .Where(u => u.Id == request.ContactId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbContact != null)
                {
                    response.Result = Mapper.Map<ContactModel>(dbContact);
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
