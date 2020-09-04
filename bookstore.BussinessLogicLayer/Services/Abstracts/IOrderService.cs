using bookstore.BussinessEnitites.Models;
using bookstore.DataTransferObject.DTOs;
using bookstore.Shared.ApiResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.BussinessLogicLayer.Services.Abstracts
{
    public interface IOrderService
    {
        Task<ApiResponse<IEnumerable<Order>>> GetOrders(uint pageNumber, uint pageSize);
        Task<ApiResponse<IEnumerable<Order>>> GetUserOrders(uint pageNumber, uint pageSize);
        Task<ApiResponse<IEnumerable<Order>>> GetUserOrder(string id);
        Task<ApiResponse<IEnumerable<Order>>> GetOrder(string id);
        Task<ApiResponse<bool>> CreateOrder(List<CreateOrderDTO> orderDTOs);
    }
}
