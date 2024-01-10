﻿using ISSA.Mapper;

namespace ISSA.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AdminMapperProfile>();
                cfg.AddProfile<StudentMapperProfile>();
                cfg.AddProfile<MentorMapperProfile>();
            });
            return services;
        }
    }
}