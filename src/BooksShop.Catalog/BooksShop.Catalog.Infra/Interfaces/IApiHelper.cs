namespace BooksShop.Catalog.Infra.Interfaces
{
    public interface IApiHelper
    {
         Task<TResponse> GetAsync<TResponse>(string url);
    }
}