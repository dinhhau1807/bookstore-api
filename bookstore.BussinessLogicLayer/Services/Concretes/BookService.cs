using AutoMapper;
using bookstore.BussinessEnitites.Models;
using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.DataAccessLayer.Repositories.Abstracts;
using bookstore.DataTransferObject.DTOs;
using bookstore.Shared.ApiResponse;
using bookstore.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.BussinessLogicLayer.Services.Concretes
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookService(IMapper mapper, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<ApiResponse<IEnumerable<Book>>> GetBooks(uint pageNumber, uint pageSize)
        {
            var books = await _bookRepository.GetListPaging(pageNumber, pageSize, o => o.Id);
            var total = await _bookRepository.CountAll();

            var result = ApiResponse<IEnumerable<Book>>.Ok(books);
            result.ExtraData = new { total };

            return result;
        }

        public async Task<ApiResponse<Book>> GetBook(int id)
        {
            var book = await FindBook(id);
            return ApiResponse<Book>.Ok(book);
        }

        public async Task<ApiResponse<bool>> CreateBook(BookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            book.CreatedAt = DateTime.UtcNow;

            var result = await _bookRepository.Insert(book);
            if (!result)
            {
                throw new AppException(StatusCodes.Status500InternalServerError, "Cannot add new book.");
            }

            return ApiResponse<bool>.Ok(result);
        }

        public async Task<ApiResponse<bool>> UpdateBook(UpdateBookDTO updateBookDTO)
        {
            var book = await FindBook(updateBookDTO.Id);
            book = _mapper.Map(updateBookDTO, book);

            var result = await _bookRepository.Update(book);
            if (!result)
            {
                throw new AppException(StatusCodes.Status500InternalServerError, "Cannot update book.");
            }

            return ApiResponse<bool>.Ok(result);
        }

        public async Task<ApiResponse<bool>> DeleteBook(int id)
        {
            var book = await FindBook(id);

            var result = await _bookRepository.Delete(book);
            if (!result)
            {
                throw new AppException(StatusCodes.Status500InternalServerError, "Cannot delete book.");
            }

            return ApiResponse<bool>.Ok(result);
        }

        #region Helpers
        private async Task<Book> FindBook(int id)
        {
            var book = await _bookRepository.GetOne(id);
            if (book == null)
            {
                throw new AppException(StatusCodes.Status404NotFound, "Book not found.");
            }

            return book;
        }
        #endregion
    }
}
