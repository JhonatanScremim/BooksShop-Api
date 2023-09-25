using AutoMapper;
using BooksShop.Catalog.Application.Interfaces;
using BooksShop.Catalog.Domain;
using BooksShop.Catalog.Infra.DTOs;
using BooksShop.Catalog.Infra.Helpers.Exceptions;
using BooksShop.Catalog.Infra.ViewModels;
using BooksShop.Catalog.Repository.Interfaces;

namespace BooksShop.Catalog.Application
{
    public class PublisherService : IPublisherService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;

        public PublisherService(IBaseRepository baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<PublisherViewModel?> CreateAsync(PublisherDTO model)
        {
            var publisher = _mapper.Map<Publisher>(model);
            _baseRepository.Create(publisher);
            
            if(!await _baseRepository.SaveChangesAsync())
                throw new BadRequestException("Could not create");
                
            return _mapper.Map<PublisherViewModel>(publisher);
        }
    }
}