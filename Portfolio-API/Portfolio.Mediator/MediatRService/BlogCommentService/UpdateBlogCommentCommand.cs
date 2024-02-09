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


namespace Portfolio.API.MediatR.MediatRService.BlogCommentService
{
    // <summary>
    // 
    // </summary>
    // <param name = "BlogComment" ></ param >
    public record UpdateBlogCommentCommand(int BlogCommentId, BlogCommentModel BlogComment) : IRequest<ServiceResponse<BlogCommentModel>>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="DbContext"></param>
    /// <param name="Mediator"></param>
    public record UpdateBlogCommentHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<UpdateBlogCommentCommand, ServiceResponse<BlogCommentModel>>
    {
        public async Task<ServiceResponse<BlogCommentModel>> Handle(UpdateBlogCommentCommand request, CancellationToken cancellationToken)
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
                    Mapper.Map(request.BlogComment, dbBlogComment);
                    dbContext.Entry(dbBlogComment).State = EntityState.Modified;
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = request.BlogComment;
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
