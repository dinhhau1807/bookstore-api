using AutoMapper;
using bookstore.BussinessEnitites.Models;
using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.DataAccessLayer.Repositories.Abstracts;
using bookstore.DataTransferObject.DTOs;
using bookstore.Shared.ApiResponse;
using bookstore.Shared.Helpers;
using bookstore.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.BussinessLogicLayer.Services.Concretes
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IHttpContextCurrentUser _httpContextCurrentUser;

        public OrderService(IMapper mapper, IBookRepository bookRepository, IOrderRepository orderRepository, IHttpContextCurrentUser httpContextCurrentUser)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _httpContextCurrentUser = httpContextCurrentUser;
        }

        public async Task<ApiResponse<IEnumerable<Order>>> GetOrders(uint pageNumber, uint pageSize)
        {
            var orders = await _orderRepository.GetListPaging(pageNumber, pageSize, o => o.Id);
            var total = await _orderRepository.CountAll();

            var result = ApiResponse<IEnumerable<Order>>.Ok(orders);
            result.ExtraData = new { total };

            return result;
        }

        public async Task<ApiResponse<IEnumerable<Order>>> GetUserOrders(uint pageNumber, uint pageSize)
        {
            var currentId = _httpContextCurrentUser.CurrentUserId;
            var orders = await _orderRepository.FindByConfitionPaging(pageNumber, pageSize, o => o.Id, o => o.UserId == currentId);
            var total = await _orderRepository.CountByCondition(o => o.UserId == currentId);

            var result = ApiResponse<IEnumerable<Order>>.Ok(orders);
            result.ExtraData = new { total };

            return result;
        }


        public async Task<ApiResponse<IEnumerable<Order>>> GetUserOrder(string id)
        {
            var currentId = _httpContextCurrentUser.CurrentUserId;
            var order = (await _orderRepository.FindByCondition(o => o.UserId == currentId && o.Id == id));
            if (!order.Any())
            {
                throw new AppException(StatusCodes.Status404NotFound, "Order not found");
            }

            return ApiResponse<IEnumerable<Order>>.Ok(order);
        }


        public async Task<ApiResponse<IEnumerable<Order>>> GetOrder(string id)
        {
            var order = (await _orderRepository.FindByCondition(o => o.Id == id));
            if (!order.Any())
            {
                throw new AppException(StatusCodes.Status404NotFound, "Order not found");
            }

            return ApiResponse<IEnumerable<Order>>.Ok(order);
        }

        public async Task<ApiResponse<bool>> CreateOrder(List<CreateOrderDTO> orderDTOs)
        {
            var currentId = _httpContextCurrentUser.CurrentUserId;
            var orderId = Guid.NewGuid().ToString();

            var orders = orderDTOs.Select(o =>
            {
                var order = _mapper.Map<Order>(o);
                order.Id = orderId;
                order.UserId = currentId.Value;
                order.CreatedAt = DateTime.UtcNow;
                return order;
            }).ToList();

            // Check valid
            await orders.ForEachAsync(async o =>
            {
                var book = await _bookRepository.GetOne(o.BookId);
                if (book == null)
                {
                    throw new AppException(StatusCodes.Status404NotFound, "Book not found");
                }

                if (book.Quantity < o.Quantity)
                {
                    throw new AppException(StatusCodes.Status400BadRequest, "Not enough book.");
                }
            });

            await orders.ForEachAsync(async o =>
            {
                var book = await _bookRepository.GetOne(o.BookId);
                book.Quantity -= o.Quantity;
                o.UnitPrice = book.UnitPrice;

                var updateBookResult = await _bookRepository.Update(book);
                var addOrderResult = await _orderRepository.Insert(o);
            });

            return ApiResponse<bool>.Ok(true);
        }
    }
}
