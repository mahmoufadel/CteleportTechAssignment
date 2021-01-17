using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using CteleportTechAssignment.Persistence;
using CteleportTechAssignment.Cache;
using CteleportTechAssignment.Application;
using Microsoft.AspNetCore.Mvc;
using CteleportTechAssignment.Core.Resources;
using System.Net;
using CteleportTechAssignment.Core.Models;
using AutoMapper;

namespace CteleportTechAssignment
{
    public static class ConfigureExtensions
    {
        public static void CorsConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IAPIService, APIService>();
            services.AddSingleton<ICacheService, CacheManager>();

            services.AddTransient<IAirportService, AirportService>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddMvc()
                    .ConfigureApiBehaviorOptions(options =>
                    {                       
                        options.InvalidModelStateResponseFactory = actionContext =>
                        {
                            var ErrorDetails = actionContext.ModelState.Values.ToList().Select(error => new ErrorDetails { StatusCode = (int)HttpStatusCode.BadRequest, Message = error.Errors.FirstOrDefault().ErrorMessage }).ToList();
                            return new BadRequestObjectResult(ErrorUtility.GetErrorDetails((int)HttpStatusCode.BadRequest, ErrorDetails));
                        };
                    });
        }
    }
}
