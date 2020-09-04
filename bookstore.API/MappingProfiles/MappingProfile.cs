using AutoMapper;
using bookstore.BussinessEnitites.Models;
using bookstore.DataTransferObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.API.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, RegisterAccountDTO>().ReverseMap();

            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, UpdateBookDTO>().ReverseMap();
        }
    }
}
