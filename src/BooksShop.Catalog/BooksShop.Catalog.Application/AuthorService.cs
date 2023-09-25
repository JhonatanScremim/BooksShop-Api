using AutoMapper;
using BooksShop.Catalog.Application.Interfaces;
using BooksShop.Catalog.Domain;
using BooksShop.Catalog.Infra.DTOs;
using BooksShop.Catalog.Infra.Helpers.Exceptions;
using BooksShop.Catalog.Infra.ViewModels;
using BooksShop.Catalog.Repository.Interfaces;

namespace BooksShop.Catalog.Application
{
    public class AuthorService : IAuthorService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        public AuthorService(IBaseRepository baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<AuthorViewModel?> CreateAsync(AuthorDTO model)
        {
            var author = _mapper.Map<Author>(model);
            _baseRepository.Create(author);
            
            if(!await _baseRepository.SaveChangesAsync())
                throw new BadRequestException("Could not create");

            return _mapper.Map<AuthorViewModel>(author);
        }
    }
}