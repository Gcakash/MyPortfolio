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
    // <summary>
    // 
    // </summary>
    // <param name = "BlogComment" ></ param >
    public record GetBlogCommentQuery : IRequest<ServiceResponse<List<BlogCommentModel>>>;

    public record GetBlogCommentHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetBlogCommentQuery, ServiceResponse<List<BlogCommentModel>>>
    {
        public async Task<ServiceResponse<List<BlogCommentModel>>> Handle(GetBlogCommentQuery request, 
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<BlogCommentModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbBlogCommentes = await dbContext.BlogComments
                    .ToListAsync(cancellationToken);

                if (dbBlogCommentes.Count > 0)
                {
                    response.Result = Mapper.Map<List<BlogCommentModel>>(dbBlogCommentes);
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
