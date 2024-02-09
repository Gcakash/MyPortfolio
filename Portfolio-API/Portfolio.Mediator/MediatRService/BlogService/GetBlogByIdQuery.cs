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


namespace Portfolio.API.MediatR.MediatRService.BlogService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="BlogId"></param>
    public record GetBlogByIdQuery(int BlogId) : IRequest<ServiceResponse<BlogPostModel>>;

    public record GetBlogByIdHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetBlogByIdQuery, ServiceResponse<BlogPostModel>>
    {
        public async Task<ServiceResponse<BlogPostModel>> Handle(GetBlogByIdQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<BlogPostModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbBlog = await dbContext.BlogPosts
                    .Where(u => u.BlogId == request.BlogId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbBlog != null)
                {
                    response.Result = Mapper.Map<BlogPostModel>(dbBlog);
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
