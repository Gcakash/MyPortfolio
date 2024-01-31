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


namespace Portfolio.API.MediatR.MediatRService.BlogService
{
    // <summary>
    // 
    // </summary>
    // <param name = "Blog" ></ param >
    public record UpdateBlogCommand(int BlogId, BlogPostModel Blog) : IRequest<ServiceResponse<BlogPostModel>>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="DbContext"></param>
    /// <param name="Mediator"></param>
    public record UpdateBlogHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<UpdateBlogCommand, ServiceResponse<BlogPostModel>>
    {
        public async Task<ServiceResponse<BlogPostModel>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
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
                    Mapper.Map(request.Blog, dbBlog);
                    dbContext.Entry(dbBlog).State = EntityState.Modified;
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = request.Blog;
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
