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
    // <summary>
    // 
    // </summary>
    // <param name = "Blog" ></ param >
    public record GetBlogQuery : IRequest<ServiceResponse<List<BlogPostModel>>>;

    public record GetBlogHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetBlogQuery, ServiceResponse<List<BlogPostModel>>>
    {
        public async Task<ServiceResponse<List<BlogPostModel>>> Handle(GetBlogQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<BlogPostModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbBloges = await dbContext.BlogPosts
                    .ToListAsync(cancellationToken);

                if (dbBloges.Count > 0)
                {
                    response.Result = Mapper.Map<List<BlogPostModel>>(dbBloges);
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
