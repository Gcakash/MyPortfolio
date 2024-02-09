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
    // <summary>
    // 
    // </summary>
    // <param name = "Feedback" ></ param >
    public record GetFeedbacksQuery : IRequest<ServiceResponse<List<FeedbackModel>>>;

    public record GetFeedbacksHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetFeedbacksQuery, ServiceResponse<List<FeedbackModel>>>
    {
        public async Task<ServiceResponse<List<FeedbackModel>>> Handle(GetFeedbacksQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<FeedbackModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbFeedbackes = await dbContext.Feedbacks
                    .ToListAsync(cancellationToken);

                if (dbFeedbackes.Count > 0)
                {
                    response.Result = Mapper.Map<List<FeedbackModel>>(dbFeedbackes);
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
