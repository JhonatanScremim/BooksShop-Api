using AutoMapper;
using BooksShop.Catalog.Application.DTOs;
using BooksShop.Catalog.Domain;

namespace BooksShop.Catalog.Application.Helpers.Mappers
{
    public class DTOToDomainMapper : Profile
    {
        public DTOToDomainMapper()
        {
            CreateMap<BookDTO, Book>();
            CreateMap<AuthorDTO, Author>();
        }
    }
}