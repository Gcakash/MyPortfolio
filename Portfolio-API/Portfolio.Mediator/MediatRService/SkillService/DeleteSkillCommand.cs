using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using Portfolio.API.Common.ServiceResponse;
using Portfolio.API.Data.ApplicationDbContext;
using Portfolio.API.Common.Enum;
using Portfolio.API.Common.Constatnt;


namespace Portfolio.API.MediatR.MediatRService.SkillService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SkillId"></param>
    public record DeleteSkillCommand(int SkillId) : IRequest<ServiceResponse<bool>>;


    public record DeleteSkillHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory, IMediator Mediator) :
        IRequestHandler<DeleteSkillCommand, ServiceResponse<bool>>
    {
        public async Task<ServiceResponse<bool>> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<bool> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbSkill = await dbContext.Skills
                    .Where(u => u.Id == request.SkillId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (dbSkill != null)
                {
                    dbContext.Skills.Remove(dbSkill); 
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = true;
                        response.Type = ServiceResponseTypes.SUCCESS;
                    }
                    else
                    {
                        errors.Add(ErrorConstant.Delete_Error);
                        response.Result = false;
                        response.Type = ServiceResponseTypes.ERROR;
                        response.ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString();
                        response.Errors = errors;
                    }
                }
                else
                {
                    errors.Add(ErrorConstant.NotFound_Error);
                    response.Result = false;
                    response.Type = ServiceResponseTypes.ERROR;
                    response.ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Errors = errors;
                }
            }
            return response;
        }
    }
}
