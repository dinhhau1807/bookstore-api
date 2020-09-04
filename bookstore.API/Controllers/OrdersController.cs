using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.BussinessEnitites.Enums;
using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.DataTransferObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderSerice;

        public OrdersController(IOrderService orderService)
        {
            _orderSerice = orderService;
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet("~/api/admin/[controller]")]
        public async Task<IActionResult> GetOrders(uint pageNumer = 1, uint pageSize = 30)
        {
            return Ok(await _orderSerice.GetOrders(pageNumer, pageSize));
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet("~/api/admin/[controller]/{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            return Ok(await _orderSerice.GetOrder(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders(uint pageNumer = 1, uint pageSize = 30)
        {
            return Ok(await _orderSerice.GetUserOrders(pageNumer, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserOrder(string id)
        {
            return Ok(await _orderSerice.GetUserOrder(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(List<CreateOrderDTO> orderDTOs)
        {
            return Ok(await _orderSerice.CreateOrder(orderDTOs));
        }
    }
}
