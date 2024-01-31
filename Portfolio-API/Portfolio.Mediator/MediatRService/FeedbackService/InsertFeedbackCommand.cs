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



namespace Portfolio.API.MediatR.MediatRService.FeedbackService
{
    public record InsertFeedbackCommand(FeedbackModel Feedback)
        : IRequest<ServiceResponse<FeedbackModel>>;

    public record InsertFeedbackHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<InsertFeedbackCommand, ServiceResponse<FeedbackModel>>
    {
        public async Task<ServiceResponse<FeedbackModel>> Handle(InsertFeedbackCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<FeedbackModel> response = new();
            List<string> errors = new();

            if (request.Feedback != null)
            {
                using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
                {
                    var dbFeedback = Mapper.Map<Feedback>(request.Feedback);
                    dbContext.Add(dbFeedback);
                    var result = await dbContext.SaveChangesAsync(cancellationToken);

                    if (result != 0)
                    {
                        request.Feedback.Id = dbFeedback.Id;
                        response.Result = request.Feedback;
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

