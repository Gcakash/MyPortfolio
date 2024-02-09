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


namespace Portfolio.API.MediatR.MediatRService.BlogPostService
{
    // <summary>
    // 
    // </summary>
    // <param name = "BlogPost" ></ param >
    public record GetActiveBlogPostsQuery : IRequest<ServiceResponse<List<BlogPostModel>>>;

    public record GetActiveBlogPostsHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetActiveBlogPostsQuery, ServiceResponse<List<BlogPostModel>>>
    {
        public async Task<ServiceResponse<List<BlogPostModel>>> Handle(GetActiveBlogPostsQuery request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<BlogPostModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbBlogPostes = await dbContext.BlogPosts
                    .Where(u => u.IsActive)
                    .ToListAsync(cancellationToken);

                if (dbBlogPostes.Count > 0)
                {
                    response.Result = Mapper.Map<List<BlogPostModel>>(dbBlogPostes);
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
