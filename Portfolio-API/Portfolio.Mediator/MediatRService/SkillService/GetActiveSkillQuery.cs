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


namespace Portfolio.API.MediatR.MediatRService.SkillService
{
    // <summary>
    // 
    // </summary>
    // <param name = "Skill" ></ param >
    public record GetActiveSkillsQuery : IRequest<ServiceResponse<List<SkillModel>>>;

    public record GetActiveSkillsHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetActiveSkillsQuery, ServiceResponse<List<SkillModel>>>
    {
        public async Task<ServiceResponse<List<SkillModel>>> Handle(GetActiveSkillsQuery request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<SkillModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbSkilles = await dbContext.Skills
                    .Where(u => u.IsActive)
                    .ToListAsync(cancellationToken);

                if (dbSkilles.Count > 0)
                {
                    response.Result = Mapper.Map<List<SkillModel>>(dbSkilles);
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
