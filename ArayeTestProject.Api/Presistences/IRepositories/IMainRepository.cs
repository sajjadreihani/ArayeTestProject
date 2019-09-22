using System.Collections.Generic;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Application.Models.Sales;

namespace ArayeTestProject.Api.Presistences.IRepositories {
    public interface IMainRepository {
        Task<List<City>> AddCities (List<City> cities);
        Task AddSales (List<Sale> sales);
        Task<List<string>> GetNames (string searchKey);
        Task<List<Sale>> GetProductNames (string searchKey);
        Task<List<City>> GetCityNames (string searchKey);
        Task<List<Sale>> GetSaleList (SaleListFilterModel filter);
        Task UpdateSale (Sale newsale);
        Task<bool> IsUsertNameExist (string userName);
        Task<bool> IsProductExist (string productId,string productName);
        Task<long> IsCityNameExist (string cityName);
        Task<long> GetLastSalePrice (SaleListFilterModel filter);
        Task AddSale (Sale sale);
        Task DeleteSale (long SaleId);
    }
}