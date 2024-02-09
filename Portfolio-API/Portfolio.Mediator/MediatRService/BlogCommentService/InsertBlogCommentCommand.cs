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



namespace Portfolio.API.MediatR.MediatRService.BlogCommentService
{
    public record InsertBlogCommentCommand(BlogCommentModel BlogComment)
        : IRequest<ServiceResponse<BlogCommentModel>>;

    public record InsertBlogCommentHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<InsertBlogCommentCommand, ServiceResponse<BlogCommentModel>>
    {
        public async Task<ServiceResponse<BlogCommentModel>> Handle(InsertBlogCommentCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<BlogCommentModel> response = new();
            List<string> errors = new();

            if (request.BlogComment != null)
            {
                using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
                {
                    var dbBlogComment = Mapper.Map<BlogComment>(request.BlogComment);
                    dbContext.Add(dbBlogComment);
                    var result = await dbContext.SaveChangesAsync(cancellationToken);

                    if (result != 0)
                    {
                        request.BlogComment.Id = dbBlogComment.Id;
                        response.Result = request.BlogComment;
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

