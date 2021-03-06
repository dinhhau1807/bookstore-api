﻿using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.BussinessLogicLayer.Services.Concretes;
using bookstore.DataAccessLayer.Repositories.Abstracts;
using bookstore.DataAccessLayer.Repositories.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookstore.BussinessLogicLayer
{
    public static class RegisterServiceExtensions
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }

        public static IServiceCollection AddBussinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
