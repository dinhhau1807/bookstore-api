using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.BussinessEnitites.Enums;
using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.DataTransferObject.DTOs;
using bookstore.Shared.ApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetBooks(uint pageNumber = 1, uint pageSize = 30)
        {
            return Ok(await _bookService.GetBooks(pageNumber, pageSize));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _bookService.GetBook(id));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> AddBook(BookDTO model)
        {
            return Ok(await _bookService.CreateBook(model));
        }

        [HttpPut]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> UpdateBook(UpdateBookDTO model)
        {
            return Ok(await _bookService.UpdateBook(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _bookService.DeleteBook(id));
        }
    }
}
