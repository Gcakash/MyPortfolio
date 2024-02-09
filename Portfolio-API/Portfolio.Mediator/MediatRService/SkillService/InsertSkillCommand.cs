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



namespace Portfolio.API.MediatR.MediatRService.SkillService
{
    public record InsertSkillCommand(SkillModel Skill)
        : IRequest<ServiceResponse<SkillModel>>;

    public record InsertSkillHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<InsertSkillCommand, ServiceResponse<SkillModel>>
    {
        public async Task<ServiceResponse<SkillModel>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<SkillModel> response = new();
            List<string> errors = new();

            if (request.Skill != null)
            {
                using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
                {
                    var dbSkill = Mapper.Map<Skill>(request.Skill);
                    dbContext.Add(dbSkill);
                    var result = await dbContext.SaveChangesAsync(cancellationToken);

                    if (result != 0)
                    {
                        request.Skill.Id = dbSkill.Id;
                        response.Result = request.Skill;
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

