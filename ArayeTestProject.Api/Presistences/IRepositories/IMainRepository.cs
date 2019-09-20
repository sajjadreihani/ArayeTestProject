using System.Collections.Generic;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;

namespace ArayeTestProject.Api.Presistences.IRepositories {
    public interface IMainRepository {
        Task<List<City>> AddCities (List<City> cities);
        Task AddSales (List<Sale> sales);
        Task<List<string>> GetNames (string searchKey);
        Task<List<string>> GetProductNames (string searchKey);
        Task<List<string>> GetCityNames (string searchKey);
    }
}