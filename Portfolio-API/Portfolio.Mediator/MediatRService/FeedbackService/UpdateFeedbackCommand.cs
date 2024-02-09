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


namespace Portfolio.API.MediatR.MediatRService.FeedbackService
{
    // <summary>
    // 
    // </summary>
    // <param name = "Feedback" ></ param >
    public record UpdateFeedbackCommand(int FeedbackId, FeedbackModel Feedback) : IRequest<ServiceResponse<FeedbackModel>>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="DbContext"></param>
    /// <param name="Mediator"></param>
    public record UpdateFeedbackHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<UpdateFeedbackCommand, ServiceResponse<FeedbackModel>>
    {
        public async Task<ServiceResponse<FeedbackModel>> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<FeedbackModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbFeedback = await dbContext.Feedbacks
                    .Where(u => u.Id == request.FeedbackId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbFeedback != null)

                {
                    Mapper.Map(request.Feedback, dbFeedback);
                    dbContext.Entry(dbFeedback).State = EntityState.Modified;
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = request.Feedback;
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
