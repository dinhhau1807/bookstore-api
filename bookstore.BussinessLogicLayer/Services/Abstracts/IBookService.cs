using bookstore.BussinessEnitites.Models;
using bookstore.DataTransferObject.DTOs;
using bookstore.Shared.ApiResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.BussinessLogicLayer.Services.Abstracts
{
    public interface IBookService
    {
        Task<ApiResponse<IEnumerable<Book>>> GetBooks(uint pageNumber, uint pageSize);
        Task<ApiResponse<Book>> GetBook(int id);
        Task<ApiResponse<bool>> CreateBook(BookDTO bookDTO);
        Task<ApiResponse<bool>> UpdateBook(UpdateBookDTO bookDTO);
        Task<ApiResponse<bool>> DeleteBook(int id);
    }
}
