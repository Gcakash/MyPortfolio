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


namespace Portfolio.API.MediatR.MediatRService.BlogCommentService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="BlogCommentId"></param>
    public record GetBlogCommentByIdQuery(int BlogCommentId) : IRequest<ServiceResponse<BlogCommentModel>>;

    public record GetBlogCommentByIdHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetBlogCommentByIdQuery, ServiceResponse<BlogCommentModel>>
    {
        public async Task<ServiceResponse<BlogCommentModel>> Handle(GetBlogCommentByIdQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<BlogCommentModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbBlogComment = await dbContext.BlogComments
                    .Where(u => u.Id == request.BlogCommentId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbBlogComment != null)
                {
                    response.Result = Mapper.Map<BlogCommentModel>(dbBlogComment);
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
