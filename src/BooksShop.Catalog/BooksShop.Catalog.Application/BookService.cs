using AutoMapper;
using BooksShop.Catalog.Application.DTOs;
using BooksShop.Catalog.Application.Interfaces;
using BooksShop.Catalog.Application.ViewModels;
using BooksShop.Catalog.Domain;
using BooksShop.Catalog.Repository.Interfaces;

namespace BooksShop.Catalog.Application
{
    public class BookService : IBookService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookService(IBaseRepository baseRepository, IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<List<BookViewModel>?> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<List<BookViewModel>>(books);
        }
        public async Task<BookViewModel?> CreateAsync(BookDTO model)
        {
            var book = _mapper.Map<Book>(model);

            if(model.AuthorsIds == null)
                throw new Exception("AuthorsIds Required!");

            var authorsIds = await _authorRepository.GetByListIdsAsync(model.AuthorsIds);

            if(authorsIds?.Count < model.AuthorsIds.Count)
                throw new Exception("Not found all authors by Ids");

            foreach(var id in model.AuthorsIds)
            {
                var authorBook = new AuthorBook(){
                    AuthorId = id,
                    Book = book
                };

                _baseRepository.CreateAsync(authorBook);
            }

            if(!await _baseRepository.SaveChangesAsync())
                throw new Exception("Could not create");

            return _mapper.Map<BookViewModel>(book);
        }
    }
}