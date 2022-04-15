using System.Reflection;
using AutoMapper;
using BooksShop.Catalog.Infra.DTOs;
using BooksShop.Catalog.Infra.Helpers.Exceptions;
using BooksShop.Catalog.Application.Interfaces;
using BooksShop.Catalog.Infra.ViewModels;
using BooksShop.Catalog.Domain;
using BooksShop.Catalog.Repository.Interfaces;
using System.Linq;
using BooksShop.Catalog.Infra.Helpers.Models;

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

        public PageList<BookViewModel>? GetPaginated(PageParams pageParams)
        {
            var queryPaginated = _bookRepository.GetPaginated(pageParams, out int totalCount);

            if (queryPaginated == null)
                return null;

            var books = _mapper.Map<List<BookViewModel>>(queryPaginated.ToList());

            return new PageList<BookViewModel>(pageParams.PageNumber, pageParams.PageSize, totalCount, books);
        }

        public async Task<List<BookViewModel>?> GetAllAsync()
        {
            return _mapper.Map<List<BookViewModel>>(await _bookRepository.GetAllAsync());
        }
        
        public async Task<BookViewModel?> CreateAsync(BookDTO model)
        {
            var book = _mapper.Map<Book>(model);

            if(model.AuthorsIds == null)
                throw new BadRequestException("AuthorsIds Required!");

            var authorsIds = await _authorRepository.GetByListIdsAsync(model.AuthorsIds);

            if(authorsIds?.Count < model.AuthorsIds.Count)
                throw new BadRequestException("Not found all authors by Ids");

                foreach (var authorBook in from id in model.AuthorsIds
                                       let authorBook = new AuthorBook()
                                       {
                                           AuthorId = id,
                                           Book = book
                                       }
                                       select authorBook)
                _baseRepository.Create(authorBook);

            if(!await _baseRepository.SaveChangesAsync())
                throw new BadRequestException("Could not create");

            return _mapper.Map<BookViewModel>(book);
        }

        
        public async Task<BookViewModel?> UpdateAsync(int bookId, BookDTO model)
        {
            var oldBook = await _bookRepository.GetByIdAsync(bookId);

            if(oldBook == null)
                throw new BadRequestException("Book is not found !");

            if(model.AuthorsIds == null || !model.AuthorsIds.Any())
                throw new BadRequestException("Author Ids required !");

            //Corrigir exceção
            _baseRepository.DeleteRange(oldBook.AuthorBooks);
            var book = _mapper.Map<Book>(model);

            foreach(var item in model.AuthorsIds)
            {
                _baseRepository.Create(new AuthorBook
                {
                    AuthorId = item,
                    BookId = book.Id
                });
            }
            _baseRepository.Update(book);
                
            if(!await _baseRepository.SaveChangesAsync())
                throw new BadRequestException("Could not update");

            return _mapper.Map<BookViewModel>(book);
        }

        public async Task<bool> Delete(int? bookId)
        {
            if(bookId == null || bookId <= 0)
                throw new BadRequestException("BookId is required!");

            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book == null)
                throw new BadRequestException("Book is not exists");

            _baseRepository.Delete(book);

            return await _baseRepository.SaveChangesAsync();
        }
    }
}