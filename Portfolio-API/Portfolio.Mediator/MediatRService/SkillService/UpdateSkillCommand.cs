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


namespace Portfolio.API.MediatR.MediatRService.SkillService
{
    // <summary>
    // 
    // </summary>
    // <param name = "Skill" ></ param >
    public record UpdateSkillCommand(int SkillId, SkillModel Skill) : IRequest<ServiceResponse<SkillModel>>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="DbContext"></param>
    /// <param name="Mediator"></param>
    public record UpdateSkillHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<UpdateSkillCommand, ServiceResponse<SkillModel>>
    {
        public async Task<ServiceResponse<SkillModel>> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
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
                    Mapper.Map(request.Skill, dbSkill);
                    dbContext.Entry(dbSkill).State = EntityState.Modified;
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = request.Skill;
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
