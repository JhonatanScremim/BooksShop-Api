using AutoMapper;
using BooksShop.Catalog.Application.DTOs;
using BooksShop.Catalog.Application.Interfaces;
using BooksShop.Catalog.Domain;
using BooksShop.Catalog.Repository.Interfaces;

namespace BooksShop.Catalog.Application
{
    public class BookService : IBookService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookService(IBaseRepository baseRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<BookDTO?> CreateAsync(BookDTO model)
        {
            var book = _mapper.Map<Book>(model);

            _baseRepository.CreateAsync(book);

            if(!await _baseRepository.SaveChangesAsync())
                return null;

            if(model.Authors != null || model.Authors.Any())
            {
                foreach(var item in model.Authors)
                {
                    if(await AddAuthorBook(book, item.Id))
                        return model;
                }
            }
        
            return null;
        }

        public async Task<bool> AddAuthorBook(Book book, int authorId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);

            _baseRepository.CreateAsync(new AuthorBook(){
                Author = author,
                Book = book
            });

            if(await _baseRepository.SaveChangesAsync())
                return true;

            return false;
        }
    }
}