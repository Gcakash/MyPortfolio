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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SkillId"></param>
    public record GetSkillByIdQuery(int SkillId) : IRequest<ServiceResponse<SkillModel>>;

    public record GetSkillByIdHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetSkillByIdQuery, ServiceResponse<SkillModel>>
    {
        public async Task<ServiceResponse<SkillModel>> Handle(GetSkillByIdQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<SkillModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbSkill = await dbContext.Skills
                    .Where(u => u.Id == request.SkillId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbSkill != null)
                {
                    response.Result = Mapper.Map<SkillModel>(dbSkill);
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
