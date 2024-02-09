﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using Portfolio.API.Common.ServiceResponse;
using Portfolio.API.Data.ApplicationDbContext;
using Portfolio.API.Common.Enum;
using Portfolio.API.Common.Constatnt;
using Portfolio.API.Models;


namespace Portfolio.API.MediatR.MediatRService.BlogService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="BlogId"></param>
    public record DeleteBlogCommand(int BlogId) : IRequest<ServiceResponse<bool>>;


    public record DeleteBlogHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory, IMediator Mediator) :
        IRequestHandler<DeleteBlogCommand, ServiceResponse<bool>>
    {
        public async Task<ServiceResponse<bool>> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<bool> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbBlog = await dbContext.BlogPosts
                    .Where(u => u.BlogId == request.BlogId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (dbBlog != null)
                {
                    dbContext.BlogPosts.Remove(dbBlog);
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = true;
                        response.Type = ServiceResponseTypes.SUCCESS;
                    }
                    else
                    {
                        errors.Add(ErrorConstant.Delete_Error);
                        response.Result = false;
                        response.Type = ServiceResponseTypes.ERROR;
                        response.ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString();
                        response.Errors = errors;
                    }
                }
                else
                {
                    errors.Add(ErrorConstant.NotFound_Error);
                    response.Result = false;
                    response.Type = ServiceResponseTypes.ERROR;
                    response.ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Errors = errors;
                }
            }
            return response;
        }
    }
}
