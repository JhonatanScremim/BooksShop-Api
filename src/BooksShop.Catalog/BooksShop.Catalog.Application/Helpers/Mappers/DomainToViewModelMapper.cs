using AutoMapper;
using BooksShop.Catalog.Application.ViewModels;
using BooksShop.Catalog.Domain;

namespace BooksShop.Catalog.Application.Helpers.Mappers
{
    public class DomainToViewModelMapper : Profile
    {
        public DomainToViewModelMapper()
        {
            CreateMap<Book, BookViewModel>()
                .AfterMap((s, d) => 
                {
                    if(s.AuthorBooks != null)
                    {
                        foreach(var item in s.AuthorBooks){
                            if(item.Author != null)
                            {
                                d.Authors.Add(new AuthorViewModel(){
                                    Id = item.Author.Id,
                                    Name = item.Author.Name
                                });
                            }   
                            
                        }
                    }
                });

            CreateMap<Publisher, PublisherViewModel>();
            CreateMap<Author, AuthorViewModel>();
        }
    }
}