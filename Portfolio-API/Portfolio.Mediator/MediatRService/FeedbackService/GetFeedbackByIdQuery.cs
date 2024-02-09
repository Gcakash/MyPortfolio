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


namespace Portfolio.API.MediatR.MediatRService.FeedbackService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="FeedbackId"></param>
    public record GetFeedbackByIdQuery(int FeedbackId) : IRequest<ServiceResponse<FeedbackModel>>;

    public record GetFeedbackByIdHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetFeedbackByIdQuery, ServiceResponse<FeedbackModel>>
    {
        public async Task<ServiceResponse<FeedbackModel>> Handle(GetFeedbackByIdQuery request, 
            CancellationToken cancellationToken)
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
                    response.Result = Mapper.Map<FeedbackModel>(dbFeedback);
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
